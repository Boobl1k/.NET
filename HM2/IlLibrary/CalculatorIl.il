//лучше не трогать
.assembly IlLibrary
{
  .ver 1:0:0:0
}

.class public abstract auto ansi sealed beforefieldinit IlLibrary.CalculatorIl extends [CORE_ASSEMBLY]System.Object
{
//енум оператион
  .class nested public sealed auto ansi Operation extends System.Enum
  {
    .field public specialname rtspecialname int32 value__
    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Unassigned = int32(0) 
    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Plus = int32(1)
    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Minus = int32(2)
    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Divide = int32(3)
    .field public static literal valuetype IlLibrary.CalculatorIl/Operation Multiply = int32(4)
  }

//поля
  .field public static initonly class [System.Runtime]System.Exception DevByZero
  .field public static initonly class [System.Runtime]System.Exception WrongOperation
  .field public static initonly class [System.Runtime]System.Exception OutOfRange
 
 //собственно калькулате
  .method public hidebysig static int32
    Calculate(int32 val1, int32 val2, valuetype IlLibrary.CalculatorIl/Operation operation ) cil managed
  {
    //если переводить в c#, тут по сути несколько if - else конструкций, идущих в ряд
  
    .maxstack 4

    ldarg val1
    ldarg val2
    
    ldc.i4 0
    ldarg operation
    beq wrongOperation
    ldc.i4 1
    ldarg operation
    beq sum 
    ldc.i4 2
    ldarg operation
    beq dif 
    ldc.i4 3
    ldarg operation
    beq divide
    ldc.i4 4
    ldarg operation
    beq mult
    
    ldsfld class [System.Runtime]System.Exception IlLibrary.CalculatorIl::OutOfRange
    throw
    
    wrongOperation:
    ldsfld class [System.Runtime]System.Exception IlLibrary.CalculatorIl::WrongOperation
    throw
    
    sum:
    add
    ret
    
    dif:
    sub
    ret
    
    divide:
    ldc.i4 0
    beq devByZero
    ldarg val2
    div
    ret
    
    mult:
    mul
    ret
    
    devByZero:
    ldsfld class [System.Runtime]System.Exception IlLibrary.CalculatorIl::DevByZero
    throw
  }

//херня для инициализации полей (в данном случае исключений)
  .method private hidebysig static specialname rtspecialname void .cctor() cil managed
  {
    .maxstack 1
    ldstr        "val2 was 0"
    newobj       instance void [System.Runtime]System.DivideByZeroException::.ctor(string)
    stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::DevByZero

    ldstr        "Wrong operation"
    newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
    stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::WrongOperation

    ldstr        "Operation was out of range"
    newobj       instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor(string)
    stsfld       class [System.Runtime]System.Exception IlLibrary.CalculatorIl::OutOfRange
    ret
  }
}
