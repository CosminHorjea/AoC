using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

class Day20 : Solution
{
    enum ModuleType
    {
        FLIP_FLOP = 0,
        CONJUNCTION = 1,
        START = 2,
    }
    enum PULSE
    {
        LOW = 0,
        HIGH = 1
    }
    class Module
    {
        public ModuleType moduleType { get; set; }
        public Dictionary<string, PULSE> LastPulses { get; set; }
        public string Label { get; set; }
        public List<string> neighbors { get; set; }
        public bool enabled { get; set; }

        public Module(string _label, ModuleType _moduleType, List<string> _neighbors)
        {
            Label = _label;
            neighbors = new List<string>(_neighbors);
            moduleType = _moduleType;
            LastPulses = new Dictionary<string, PULSE>();
            enabled = false;
        }
        public PULSE getLastSignal(string l, PULSE c)
        {

            if (LastPulses.TryGetValue(l, out var pulse))
            {
                LastPulses[l] = c;
                return pulse;
            }
            LastPulses.Add(l, c);
            return PULSE.LOW;
        }
        public void setPulse(string l, PULSE c)
        {

            if (LastPulses.TryGetValue(l, out var pulse))
            {
                LastPulses[l] = c;
                return;
            }
            LastPulses.Add(l, c);
        }
    }
    public string Part1()
    {
        var content = File.ReadAllLines("Inputs/Day20.test");
        Dictionary<string, Module> modules = new Dictionary<string, Module>();
        Dictionary<string, List<string>> inputs = new Dictionary<string, List<string>>();
        foreach (var line in content)
        {
            var parts = line.Split(" -> ");
            var label = parts[0];
            var next = parts[1].Trim().Split(", ").ToList();
            if (label == "broadcaster")
            {
                modules.Add(label, new Module(label, ModuleType.START, next));
            }
            else
            {
                switch (label[0])
                {
                    case '%':
                        modules.Add(label.Trim('%'), new Module(label.Trim('%'), ModuleType.FLIP_FLOP, next));
                        break;
                    case '&':
                        modules.Add(label.Trim('&'), new Module(label.Trim('&'), ModuleType.CONJUNCTION, next));
                        break;
                    default:
                        throw new Exception("Well shiiiiit");
                }
            }
            foreach (var n in next)
            {
                if (!inputs.TryGetValue(n, out var val))
                {
                    inputs.Add(n, new List<string>());
                };
                inputs[n].Add(label.Trim().Trim('&').Trim('%'));
            }
        }
        foreach (var i in inputs)
        {
            if (modules[i.Key].moduleType != ModuleType.CONJUNCTION)
            {
                continue;
            }
            foreach (var val in i.Value)
            {
                modules[i.Key].LastPulses.Add(val, PULSE.LOW);
            }
        }
        int lo = 0;
        int hi = 0;
        for (int i = 0; i < 1000; i++)
        {
            var (low, high) = GetCount(modules);
            lo += low;
            hi += high;
        }
        return $"{lo * hi}";
    }

    private static (int, int) GetCount(Dictionary<string, Module> modules)
    {
        Queue<(PULSE, string)> q = new Queue<(PULSE, string)>();
        q.Enqueue((PULSE.LOW, "broadcaster"));
        int lo = 0;
        int hi = 0;
        while (q.Count > 0)
        {
            var curr = q.Dequeue();
            if (!modules.TryGetValue(curr.Item2, out var module))
            {
                continue;
            }
            switch (curr.Item1)
            {
                case PULSE.LOW:
                    lo++;
                    break;
                case PULSE.HIGH:
                    hi++;
                    break;
            }
            var next_pulse = curr.Item1;
            if (module.moduleType == ModuleType.START)
            {
                next_pulse = PULSE.LOW;
            }
            else if (module.moduleType == ModuleType.FLIP_FLOP)
            {
                if (curr.Item1 == PULSE.HIGH)
                {
                    continue;
                }
                next_pulse = module.enabled ? PULSE.LOW : PULSE.HIGH;
                module.enabled = !module.enabled;
            }
            else if (module.moduleType == ModuleType.CONJUNCTION)
            {
                module.setPulse(module.Label, curr.Item1);
                if (module.LastPulses.All(a => a.Value == PULSE.HIGH))
                {
                    next_pulse = PULSE.LOW;
                }
                else
                {
                    next_pulse = PULSE.HIGH;
                }
            }
            else
            {
                throw new Exception("Wel shit 2");
            }

            foreach (var n in module.neighbors)
            {
                q.Enqueue((next_pulse, n));
            }
        }
        return (lo, hi);
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}