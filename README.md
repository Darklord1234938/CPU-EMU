# CPU Emulator
 
A custom register-based CPU emulator built from scratch in C#.
 
---
 
## What is it?
 
A software implementation of a fictional CPU architecture. Every component is built manually — the program counter, memory system, flag register, fetch/decode cycle and stack. No CPU emulation libraries or frameworks — just C# and computer architecture knowledge.
 
Inspired by real processors like the MOS 6502 (NES, Atari 2600) and the architecture of classic 8-bit computers.
 
---
 
## How a CPU works
 
A CPU runs in a continuous loop called the **fetch/decode/execute cycle**:
 
```
┌─────────────────────────────────────────┐
│                                         │
│   FETCH → DECODE → EXECUTE → FETCH...  │
│                                         │
└─────────────────────────────────────────┘
```
 
**Fetch** — Read the next instruction from memory at the program counter address
 
**Decode** — Identify what the instruction means
 
**Execute** — Perform the operation
 
QiByte implements this cycle in C# with a real memory system and program counter.
 
---
 
## Architecture
 
```
┌──────────────────────────────────────────┐
│                  CPU                     │
│                                          │
│   PC (Program Counter)                   │
│   ┌────────────────────┐                 │
│   │ 0x0000             │──▶ Points to    │
│   └────────────────────┘    next instr   │
│                                          │
│   FLAGS                                  │
│   ┌──────┬───────┬──────────┬──────────┐ │
│   │ ZERO │ CARRY │ NEGATIVE │ OVERFLOW │ │
│   └──────┴───────┴──────────┴──────────┘ │
│                                          │
│   STACK                                  │
│   ┌────────────────────┐                 │
│   │ Token stack        │                 │
│   └────────────────────┘                 │
└──────────────────┬───────────────────────┘
                   │
        ┌──────────┴──────────┐
        ▼                     ▼
┌──────────────┐     ┌──────────────┐
│     RAM      │     │     ROM      │
│   64KB       │     │   read only  │
└──────────────┘     └──────────────┘
```
 
---
 
## Components
 
### Program Counter
- Tracks current execution position in memory
- `Fetch()` — reads one byte and advances PC
- `FetchWord()` — reads two bytes (little-endian) and advances PC twice
 
```csharp
// Little-endian word fetch
byte low  = Fetch();   // low byte first
byte high = Fetch();   // high byte second
ushort word = (ushort)((high << 8) | low);
```
 
### Memory (RAM)
- 64KB addressable memory
- Byte-level and word-level read/write
- Little-endian word storage
 
### CPU Flags
Real CPUs use flags to track the result of operations:
 
| Flag | Triggers when |
|------|--------------|
| Zero | Result equals 0 |
| Carry | Result exceeds 255 (overflow for unsigned) |
| Negative | Result is negative |
| Overflow | Signed arithmetic overflow |
 
### Token System
- Classifies values as NUM, ID, STRING, CHAR, BOOL or LABEL
- Shared with the QiByte VM
 
---
 
## Instruction Set (planned)
 
```
0x01  MOV   A, n      Move value into register A
0x02  ADD   A, n      Add value to register A
0x03  STORE A, [addr] Store register A at memory address
0xFF  HLT             Halt execution
```
 
Example program in binary:
```
0x01, 0x0A,        // MOV  A, 10
0x02, 0x05,        // ADD  A, 5
0x03, 0x00, 0x20,  // STORE A, [0x2000]
0xFF               // HLT
```
 
---
 
## Memory Dump
 
Built-in hex memory dump for debugging — shows address and value in hexadecimal:
 
```
0000: 01 0A: 02 0005: 03 0006: 00 0007: 20 0008: FF
```
 
---
 
## Project Status
 
| Feature | Status |
|---------|--------|
| Program counter | ✅ Working |
| RAM read/write | ✅ Working |
| Word fetch (little-endian) | ✅ Working |
| CPU flags | ✅ Working |
| Token system | ✅ Working |
| Memory dump | ✅ Working |
| Opcode dispatch | 🔄 In progress |
| Register system | 🔄 In progress |
| Full instruction set | 📋 Planned |
| Program loader | 📋 Planned |
| Interrupt handling | 📋 Planned |
 
---
 
## Tech Stack
 
- **Language:** C#
- **Framework:** .NET
- **IDE:** Visual Studio
 
---
 
## What I Learned
 
- How the fetch/decode/execute cycle works in real CPUs
- Little-endian vs big-endian memory layout
- How CPU flags track arithmetic results
- Memory addressing with `ushort` (16-bit)
- How real programs are stored and executed as raw bytes
- The relationship between assembly language and machine code
 
---
 
## Background
 
This project is directly informed by hands-on experience writing **6502 assembly** for the Atari 2600 and NES — understanding real hardware at the instruction level before building a software emulation of it.
 
---
 
## Roadmap
 
This CPU is being built to power [V-Console](../V-Console), a custom virtual game console. [QiByte](../QiByte) will serve as the assembly language that compiles to this CPU's instruction set.
 
```
QiByte (.qib source)
        │
        ▼
QiByte Compiler
        │
        ▼
Binary bytecode (.bin)
        │
        ▼
CPU Emulator (fetch/decode/execute)
        │
        ▼
V-Console (GPU + APU + Input)
        │
        ▼
Output on screen
```
 
The full vision is a complete custom computing platform — language, compiler, CPU and console — built entirely from scratch.
 
---
 
## Related Projects
 
- [V-Console](https://github.com/Darklord1234938/V-console) — The virtual console this CPU powers
- [QiByte](https://github.com/Darklord1234938/QiByte) — The stack VM and assembly language for this CPU
 
---
 
## Author
 
**Quidon Roethof** — Software Developer, Netherlands
 
*Built to understand how CPUs actually work at a fundamental level — informed by real 6502 assembly experience on Atari 2600 and NES hardware.*