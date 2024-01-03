using ConsoleEmulator;
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
    private bool _coinInserted = false;
    private bool _start = false;
    private bool _left = false;
    private bool _right = false;
    private bool _shot = false;

    public Form1()
    {
        _cpu = new CPU8080
        {
            Input = Input
        };
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

        byte[] vram = new byte[7168];
        Array.Copy(_cpu.Memory, 0x2400, vram, 0, 7168);
        Array.Reverse(vram);

        IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(vram, 0);
        using (Bitmap bmp = new(256, 224, 32, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = (Image)bmp.Clone();
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

    private byte Input(byte port)
    {
        if (port == 0x01)
        {
            byte result = 0x08;
            if (_coinInserted)
            {
                result |= 0x01;
                _coinInserted = false;
            }
            if (_start)
            {
                result |= 0x04;
            }
            if (_shot)
            {
                result |= 0x10;
            }
            if (_left)
            {
                result |= 0x20;
            }
            if (_right)
            {
                result |= 0x40;
            }
            return result;
        }
        return 0;
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
        if (e.KeyData == Keys.C)
        {
            _coinInserted = true;
        }
        if (e.KeyData == Keys.Enter)
        {
            _start = false;
        }
        if (e.KeyData == Keys.Left)
        {
            _left = false;
        }
        if (e.KeyData == Keys.Right)
        {
            _right = false;
        }
        if (e.KeyData == Keys.Up)
        {
            _shot = false;
        }
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Enter)
        {
            _start = true;
        }
        if (e.KeyData == Keys.Left)
        {
            _left = true;
        }
        if (e.KeyData == Keys.Right)
        {
            _right = true;
        }
        if (e.KeyData == Keys.Up)
        {
            _shot = true;
        }
    }
}
