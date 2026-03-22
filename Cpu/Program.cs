using Cpu;
using static Cpu.Program;

namespace Cpu
{
    
   internal class Program
   {
       static bool runing = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            while (runing)
            {
                byte opCode = PcPointer.Fecth();
                MemoryDump((ushort)0x0000, 8);
            }
        }

        static void MemoryDump(ushort _start, int _lenght)
        {
            for (int i = 0; i < _lenght; i++)
            {
                ushort address = (ushort)(_start + i);
                Console.Write($"{address:X4}: {Ram.Read(address):X2}");

                if (i + 1 % 8 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        static public void LoadProgram(byte[] _prgram, ushort _start = 0x0000)
        {
            Ram.ToRam(64);
            for (int i = 0; i < _prgram.Length; i++)
            {
                Ram.Write((ushort)(_start + i), _prgram[i]);
            }

        }

        // ASK CHAT GTP WHAT IT DOES
    }

    public class ProgramFile{
        public byte[] program = new byte[]
        {
            0x01, 0x0A, // Move A, 10
            0x02, 0x05, // Add A, 5
            0x03, 0x00, 0x20, // STORE A, [0X2000]
            0Xff // HLT
        };
    }

public enum TokeType
{
    None,
    Id, //
    Num, //
    Label, //
    String, //
    Bool, //
    Char, //
}

public enum FlagType
{
    None,
    Zero,
    Carry,
    Negative,
    Overflow

}

public class Token
{
    TokeType tt;
    string value = "";

    public Token(string _value)
    {
        value = _value;

        if (float.TryParse(value, out float v))
        {
            tt = TokeType.Num;
        }
        else if (value[0] == '"' && value[value.Length] == '"')
        {
            tt = TokeType.String;
        }
        else if (value[0] == '\'' && value[2] == '\'' && value.Length == 3)
        {
            tt = TokeType.Char;
        }
        else if (value == "Label")
        {
            tt = TokeType.Label;
        }
        else if (value == "False" || value == "True")
        {
            tt = TokeType.Bool;
        }
        else
        {
            tt = TokeType.Id;
        }
    }
}

// ASK CHAT GTP WHAT IT DOES
public static class PcPointer
{
    static ushort pc = 0x0000;

    static public byte Fecth()
    {
        return Ram.Read(pc++);

    }

    static public ushort FecthWord()
    {
        byte low = Fecth();
        byte high = Fecth();
        return (ushort)((high << 8 | low));

    }
}


public class Flag
{
    FlagType ft;

    public Flag(FlagType _ft)
    {
        ft = _ft;
    }

    public bool Check(float _f)
    {
        switch (ft)
        {
            case FlagType.None:
                break;
            case FlagType.Zero:
                if (_f == 0)
                    return true;
                break;
            case FlagType.Carry:
                if (_f > 255)
                    return true;
                break;
            case FlagType.Negative:
                return false; // No how to do that
                break;
            case FlagType.Overflow:
                return false; // No how to do that
                break;
        }
        return false;
    }
}

public static class Stackk
{
    static Stack<Token> stack = new Stack<Token>();
    public static int pointerToStack;

    public static void Pop()
    {
        stack.Pop();
        pointerToStack++;
    }

    public static void Push(Token _t)
    {
        if (stack.Count > 0)
        {
            stack.Push(_t);
            pointerToStack--;
        }
    }

}

public static class Ram
{
    public static List<byte> ram = new List<byte>();

    public static void ToRam(int _kb)
    {
        ram.Clear();

        int kb = _kb * 1024;
    }

    public static byte Read(ushort _adress)
    {
        return ram[_adress];
    }

    public static void Write(ushort _adress, byte _value)
    {
        ram[_adress] = _value;
    }

    public static ushort ReadWord(ushort _adress)
    {
        ushort low = Read(_adress);
        ushort high = Read((ushort)(_adress + 1));
        return (ushort)((high << 8) | low);
    }

    public static void WriteWord(ushort _adress, byte _value)
    {
        Write(_adress, (byte)(_value & 0xFF));
        Write((ushort)(_adress + 1), (byte)(_value >> 8));
    }
}
    }