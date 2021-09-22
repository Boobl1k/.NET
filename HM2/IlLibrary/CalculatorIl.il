.assembly IlLibrary
{
  .ver 1:0:0:0
}

.class public abstract auto ansi sealed beforefieldinit IlLibrary.CalculatorIl
  extends [CORE_ASSEMBLY]System.Object
{
  .class nested public sealed auto ansi
    Operation
      extends System.Enum
  {

    .field public specialname rtspecialname int32 value__

    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Unassigned = int32(0) // 0x00000000

    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Plus = int32(1) // 0x00000001

    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Minus = int32(2) // 0x00000002

    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Divide = int32(3) // 0x00000003

    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Multiply = int32(4) // 0x00000004
  } // end of class Operation

  
  .field public static initonly class [System.Runtime]System.Exception DevByZero

  .field public static initonly class [System.Runtime]System.Exception WrongOperation

  .field public static initonly class [System.Runtime]System.Exception OutOfRange

  .method public static int32 CalculateForCS(int32 val1, int32 val2, int32 operationNumber) cil managed
  {
    .maxstack 3
    ldstr "[IL] : Calling Calculator.Calculate with operation: "
    ldarg.s operationNumber
    box IlLibrary.CalculatorIl/Operation
    callvirt instance string [System.Runtime]System.Object::ToString()
    call string [System.Runtime]System.String::Concat(string, string)
    call void [System.Console]System.Console::WriteLine(object)
    
    ldarg.s val1
    ldarg.s val2
    ldarg.s operationNumber
    call int32 IlLibrary.CalculatorIl::Calculate(int32,int32,valuetype IlLibrary.CalculatorIl/Operation)
    ret
  }

  .method public hidebysig static int32
    Calculate(
      int32 val1,
      int32 val2,
      valuetype IlLibrary.CalculatorIl/Operation operation
    ) cil managed
  {
    .maxstack 2

    IL_0000: ldarg.2      // operation
    IL_0001: switch       (IL_0035, IL_001c, IL_0020, IL_0028, IL_0024)
    IL_001a: br.s         IL_003b

    // [27 21 - 27 40]
    IL_001c: ldarg.0      // val1
    IL_001d: ldarg.1      // val2
    IL_001e: add
    IL_001f: ret

    // [29 21 - 29 40]
    IL_0020: ldarg.0      // val1
    IL_0021: ldarg.1      // val2
    IL_0022: sub
    IL_0023: ret

    // [31 21 - 31 40]
    IL_0024: ldarg.0      // val1
    IL_0025: ldarg.1      // val2
    IL_0026: mul
    IL_0027: ret

    // [34 21 - 34 35]
    IL_0028: ldarg.1      // val2
    IL_0029: brtrue.s     IL_0031

    // [35 25 - 35 41]
    IL_002b: ldsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::DevByZero
    IL_0030: throw

    // [36 21 - 36 40]
    IL_0031: ldarg.0      // val1
    IL_0032: ldarg.1      // val2
    IL_0033: div
    IL_0034: ret

    // [39 21 - 39 42]
    IL_0035: ldsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::WrongOperation
    IL_003a: throw

    // [41 21 - 41 38]
    IL_003b: ldsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::OutOfRange
    IL_0040: throw

  } // end of method Calculator::Calculate

  .method private hidebysig static specialname rtspecialname void
    .cctor() cil managed
  {
    .maxstack 8

    // [9 9 - 9 94]
    IL_0000: ldstr        "val2 was 0"
    IL_0005: newobj       instance void [System.Runtime]System.DivideByZeroException::.ctor(string)
    IL_000a: stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::DevByZero

    // [10 9 - 10 100]
    IL_000f: ldstr        "Wrong operation"
    IL_0014: newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
    IL_0019: stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::WrongOperation

    // [11 9 - 11 118]
    IL_001e: ldstr        "Operation was out of range"
    IL_0023: newobj       instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor(string)
    IL_0028: stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::OutOfRange
    IL_002d: ret

  } // end of method Calculator::.cctor
  
}
