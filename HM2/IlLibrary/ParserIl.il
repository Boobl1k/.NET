.class public abstract sealed auto ansi beforefieldinit IlLibrary.ParserIl extends [System.Runtime]System.Object
{
  .method public hidebysig static valuetype IlLibrary.CalculatorIl/Operation
    ParseCalculatorOperation(string arg) cil managed
  {
    .maxstack 2

///////////////////проверка аргумента, при нахождении нужного значения переход в соответствующую метку
    ldarg arg
    ldstr "+"
    call bool System.String::op_Equality(string, string)
    brtrue plus
    
    ldarg arg
    ldstr "-"
    call bool System.String::op_Equality(string, string)
    brtrue minus
    
    ldarg arg
    ldstr "*"
    call bool System.String::op_Equality(string, string)
    brtrue mult
    
    ldarg arg
    ldstr "/"
    call bool System.String::op_Equality(string, string)
    brtrue divide

    //default
    ldc.i4 0
    ret
////////////////////////////////

    plus:
    ldc.i4 1
    ret
    
    minus:
    ldc.i4 2
    ret
    
    mult:
    ldc.i4 4
    ret
    
    divide:
    ldc.i4 3
    ret
  }

  .method public hidebysig static bool TryParsOrQuit(string str, [out] int32& result) cil managed
  {
    .maxstack 2

    ldarg str
    ldarg result
    call bool [System.Runtime]System.Int32::TryParse(string, int32&)
    brfalse isNotInt
    ldc.i4 1
    ret

    isNotInt:
    ldstr        "value is not int. The value was "
    ldarg str      
    call string [System.Runtime]System.String::Concat(string, string)
    call void [System.Console]System.Console::WriteLine(string)

    ldc.i4 0
    ret

  } // end of method Parser::TryParsOrQuit
} // end of class IlLibrary.Parser
