using ConsoleEmulator;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinFormsEmulator;

public partial class Form1 : Form
{
    private readonly CPU8080 _cpu;
    private readonly System.Timers.Timer _cpuTimer;
    private readonly System.Timers.Timer _screenTimer;
    private readonly Stopwatch _stopwatch = new();
    private long _lastCpuStep = 0;
    private long _lastInterrupt96 = -9524;
    private long _lastInterrupt224 = 0;
    private readonly ArcadeInput _arcadePort1 = new();
    private readonly ShiftOffsetDevice _shiftOffsetDevice = new();
    private readonly ShiftDevice _shiftDevice;

    public Form1()
    {
        _cpu = new();

        _shiftDevice = new(_shiftOffsetDevice);

        _cpu.AddOutputDevice(2, _shiftOffsetDevice);
        _cpu.AddOutputDevice(3, new SoundDevice(3));
        _cpu.AddOutputDevice(4, _shiftDevice);
        _cpu.AddOutputDevice(5, new SoundDevice(5));

        _cpu.AddInputDevice(1, _arcadePort1);
        _cpu.AddInputDevice(3, _shiftDevice);
        _cpuTimer = new()
        {
            Interval = 1
        };
        _cpuTimer.Elapsed += OnCpuTimer;
        _screenTimer = new()
        {
            Interval = TimeSpan.FromMicroseconds(16666).TotalMilliseconds
        };
        _screenTimer.Elapsed += OnScreenTimer;

        InitializeComponent();
    }

    private void OnScreenTimer(object? sender, System.Timers.ElapsedEventArgs e)
    {
        _screenTimer.Enabled = false;

        byte[] vram = ArrayPool<byte>.Shared.Rent(7168);
        try
        {
            _cpu.Memory.AsSpan()[0x2400..].CopyTo(vram.AsSpan());
            vram.AsSpan()[..7168].Reverse();

            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(vram, 0);
            using Bitmap bmp = new(256, 224, 32, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = (Image)bmp.Clone();
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(vram);
        }

        _screenTimer.Enabled = true;
    }

    private void OnCpuTimer(object? sender, System.Timers.ElapsedEventArgs e)
    {
        _cpuTimer.Enabled = false;

        long elapsed = _stopwatch.ElapsedMilliseconds;
        long diff = elapsed - _lastCpuStep;
        long diffInterrupt = elapsed - _lastInterrupt96;
        if (diffInterrupt > _screenTimer.Interval)
        {
            _cpu.HandleInterrupt(1);
            _lastInterrupt96 = elapsed;
        }
        diffInterrupt = elapsed - _lastInterrupt224;
        if (diffInterrupt > _screenTimer.Interval)
        {
            _cpu.HandleInterrupt(2);
            _lastInterrupt224 = elapsed;
        }

        // 2 MHz = 2000 cycles per milliseconds
        long cyclesToRun = 2000 * diff;
        cyclesToRun += _cpu.Cycles;
        while (_cpu.Cycles < cyclesToRun)
        {
            int done = _cpu.Step();
            if (done != 0)
            {
                _cpuTimer.Stop();
                _screenTimer.Stop();
                return;
            }
        }

        _lastCpuStep = elapsed;
        _cpuTimer.Enabled = true;
    }

    private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = openFileDialog1.ShowDialog();
        if (result != DialogResult.OK)
        {
            return;
        }
        byte[] buffer = new byte[0x4000];
        using (Stream romStream = openFileDialog1.OpenFile())
        {
            romStream.Read(buffer, 0, buffer.Length);
        }
        _cpu.Init(buffer, 0);
        _cpu.SystemCall = null;

        _cpuTimer.Start();
        _screenTimer.Start();
        _stopwatch.Reset();
        _lastCpuStep = 0;
        _stopwatch.Start();
    }

    private void OnCpuDiagnoseClick(object sender, EventArgs e)
    {
        var cpuDiagnosePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        if (cpuDiagnosePath == null)
        {
            return;
        }

        cpuDiagnosePath = Path.Combine(cpuDiagnosePath, "cpudiag.bin");
        if (!File.Exists(cpuDiagnosePath))
        {
            return;
        }

        byte[] buffer = new byte[0x4000];
        using (Stream romStream = new FileStream(cpuDiagnosePath, FileMode.Open, FileAccess.Read))
        {
            romStream.Read(buffer, 0x0100, 0x3F00);
        }
        _cpu.Init(buffer, 0x100);
        _cpu.SystemCall = new CPMSystemCall();

        _cpuTimer.Start();
        _screenTimer.Start();
        _stopwatch.Reset();
        _lastCpuStep = 0;
        _stopwatch.Start();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        AllocConsole();
    }

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AllocConsole();

    private void Form1_KeyUp(object sender, KeyEventArgs e)
    {
        _arcadePort1.KeyUp(e.KeyData);
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        _arcadePort1.KeyDown(e.KeyData);
    }
}
