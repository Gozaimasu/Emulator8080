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

    public Form1()
    {
        _cpu = new CPU8080();
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
            }
        }

        _lastCpuStep = elapsed;
        _cpuTimer.Enabled = true;
    }

    private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
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
}
