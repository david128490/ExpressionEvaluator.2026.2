using Backend;

var infix = "4*5/(4+6)";
Console.WriteLine($"Infix = {infix}, Result = {ExpressionEvaluator.Evaluate(infix):N5}"); // 2

var infix2 = "4*(5+6-(8/2^3)-7)-1";
Console.WriteLine($"Infix = {infix2}, Result = {ExpressionEvaluator.Evaluate(infix2):N5}"); // 11

var infix3 = "4*7^(1/3)*7*((1+9)/3*7^4)";
Console.WriteLine($"Infix = {infix3}, Result = {ExpressionEvaluator.Evaluate(infix3):N5}"); // 428,675.12518474100 

var infix4 = "144^(1/2)";
Console.WriteLine($"Infix = {infix4}, Result = {ExpressionEvaluator.Evaluate(infix4):N5}"); // 12
var infix5 = "125+37";
Console.WriteLine($"Infix = {infix5}, Result = {ExpressionEvaluator.Evaluate(infix5):N5}");

var infix6 = "12.5*3.2";
Console.WriteLine($"Infix = {infix6}, Result = {ExpressionEvaluator.Evaluate(infix6):N5}");
var infix7 = "(12.5+7.5)*2";
Console.WriteLine($"Infix = {infix7}, Result = {ExpressionEvaluator.Evaluate(infix7):N5}");
var infix8 = "125.75+37.25";
Console.WriteLine($"Infix = {infix8}, Result = {ExpressionEvaluator.Evaluate(infix8):N5}");
var infix9 = "50.5-12.25";
Console.WriteLine($"Infix = {infix9}, Result = {ExpressionEvaluator.Evaluate(infix9):N5}");

var infix10 = "25.5/2.5";
Console.WriteLine($"Infix = {infix10}, Result = {ExpressionEvaluator.Evaluate(infix10):N5}");