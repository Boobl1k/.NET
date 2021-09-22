.class public abstract sealed auto ansi beforefieldinit
  IlLibrary.ParserIl
    extends [System.Runtime]System.Object
{

  .method public hidebysig static valuetype IlLibrary.CalculatorIl/Operation
    ParseCalculatorOperation(
      string arg
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] valuetype IlLibrary.CalculatorIl/Operation V_0
    )

    // [8 13 - 15 14]
    IL_0000: ldarg.0      // arg
    IL_0001: ldstr        "+"
    IL_0006: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_000b: brtrue.s     IL_0036
    IL_000d: ldarg.0      // arg
    IL_000e: ldstr        "-"
    IL_0013: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0018: brtrue.s     IL_003a
    IL_001a: ldarg.0      // arg
    IL_001b: ldstr        "*"
    IL_0020: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0025: brtrue.s     IL_003e
    IL_0027: ldarg.0      // arg
    IL_0028: ldstr        "/"
    IL_002d: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0032: brtrue.s     IL_0042
    IL_0034: br.s         IL_0046

    // [10 24 - 10 49]
    IL_0036: ldc.i4.1
    IL_0037: stloc.0      // V_0
    IL_0038: br.s         IL_0048

    // [11 24 - 11 50]
    IL_003a: ldc.i4.2
    IL_003b: stloc.0      // V_0
    IL_003c: br.s         IL_0048

    // [12 24 - 12 53]
    IL_003e: ldc.i4.4
    IL_003f: stloc.0      // V_0
    IL_0040: br.s         IL_0048

    // [13 24 - 13 51]
    IL_0042: ldc.i4.3
    IL_0043: stloc.0      // V_0
    IL_0044: br.s         IL_0048

    // [14 22 - 14 29]
    IL_0046: ldc.i4.0
    IL_0047: stloc.0      // V_0

    IL_0048: ldloc.0      // V_0
    IL_0049: ret

  } // end of method Parser::ParseCalculatorOperation

  .method public hidebysig static bool
    TryParsOrQuit(
      string str,
      [out] int32& result
    ) cil managed
  {
    .maxstack 8

    // [19 13 - 19 47]
    IL_0000: ldarg.0      // str
    IL_0001: ldarg.1      // result
    IL_0002: call         bool [System.Runtime]System.Int32::TryParse(string, int32&)
    IL_0007: brfalse.s    IL_000b

    // [19 48 - 19 60]
    IL_0009: ldc.i4.1
    IL_000a: ret

    // [20 13 - 20 73]
    IL_000b: ldstr        "value is not int. The value was "
    IL_0010: ldarg.0      // str
    IL_0011: call         string [System.Runtime]System.String::Concat(string, string)
    IL_0016: call         void [System.Console]System.Console::WriteLine(string)

    // [21 13 - 21 28]
    IL_001b: ldc.i4.0
    IL_001c: ret

  } // end of method Parser::TryParsOrQuit
} // end of class IlLibrary.Parser
