Imports AI_ZX81.Compiler
Imports Newtonsoft.Json.JsonConvert


Namespace Compiler

    ''' <summary>
    ''' To be Used as a Node in a Syntax Tree
    ''' </summary>
    Public MustInherit Class Expression
        Public Expr As String
        Public MustOverride Function Evaluate(ByRef ParentEnvironment As ZX81_RAM) As String
        Public Function GetExpr() As String
            Return Expr
        End Function
        ''' <summary>
        ''' Type = "Parameter" / "Variable Assignment"
        ''' </summary>
        Public NodeType As String
        Public Sub New(ByRef NodeType As String)
            Me.NodeType = NodeType
        End Sub
        Public Function ToJson() As String
            Return SerializeObject(Me)

        End Function
    End Class
    ''' <summary>
    ''' Used For values ie:Integers
    ''' </summary>
    Public MustInherit Class ConstantExpression
        Inherits Expression
        ''' <summary>
        ''' Used as a literal for the final data item
        ''' Value Held ie: 5 or CAT
        ''' </summary>
        Public iValue As String
        ''' <summary>
        ''' ie: Integer / String
        ''' </summary>
        Public VarType As String

        Public Overridable Function GetValue() As String
            Return iValue

        End Function

        Public Overrides Function Evaluate(ByRef ParentEnvironment As ZX81_RAM) As String
            Return GetValue()
        End Function

        Public Sub New(ByRef NodeType As String, ByRef Value As String, ByRef Type As String)
            MyBase.New(NodeType)
            iValue = Value
            VarType = Type
            Expr = iValue
        End Sub
        Public Sub New(ByRef NodeType As String)
            MyBase.New(NodeType)
        End Sub
    End Class
    Public MustInherit Class UnaryExpression
        Inherits ConstantExpression
        Public Identifier As String
        Public ParentEnv As ZX81_RAM
        Public Sub New(ByRef NodeType As String, ByRef iName As String, ByRef Value As String, ByRef Type As String, ByRef Env As ZX81_RAM)
            MyBase.New(NodeType, Value, Type)
            Me.iValue = Value
            Me.Identifier = iName
            Me.VarType = Type
            Me.ParentEnv = Env


        End Sub

        Public Overrides Function Evaluate(ByRef ParentEnvironment As ZX81_RAM) As String
            If ParentEnvironment.CheckVar(GetName) = True Then
                Me.iValue = ParentEnvironment.GetVar(Me.GetName)
                Return GetValue()
            End If
            Return MyBase.Evaluate(ParentEnvironment)
        End Function

        Public Overrides Function GetValue() As String
            Return MyBase.GetValue()
        End Function
        Public Function GetName() As String
            Return Identifier
        End Function
    End Class
    ''' <summary>
    ''' The Binary Expression is a function,
    ''' the execution of the node structure is performed by the get data functions
    ''' This class must be inherited and its Get Data function Implemented With its custom
    ''' Used for Functions A+1 = iOperator = "+" Nodetype = "ADD"  
    ''' (LeftNode = ParameterExpression(Parameter/5/a/int)) 
    ''' (RightNode = ConstantExpression (number/1/int/))
    ''' </summary>
    Public MustInherit Class BinaryExpression
        Inherits UnaryExpression
        ''' <summary>
        ''' Operator such as "+" or "DIM" 
        ''' </summary>
        Public iOperator As String
        ''' <summary>
        ''' (LeftNode = ParameterExpression(Parameter/5/a/int))  
        ''' </summary>
        Public Left As Expression
        ''' <summary>
        ''' (RightNode = ConstantExpression (number/1/int/))
        ''' </summary>
        Public Right As Expression


        Protected Sub New(ByRef NodeType As String, ByRef iName As String, ByRef Value As String, ByRef Type As String, ByRef Env As ZX81_RAM)
            MyBase.New(NodeType, iName, Value, Type, Env)

        End Sub
    End Class
    ''' <summary>
    ''' A body is a list of statements
    ''' Which is a list of Binary Expressions. 
    ''' Essentially Each Binary Expression is a single Segment of Code
    ''' 
    ''' </summary>
    Public MustInherit Class Body
        Inherits Expression
        Public Statements As New List(Of Expression)

        Public Sub New(ByRef Nodetype As String, ByRef Statment As Expression)
            MyBase.New(Nodetype)
            Statements.Add(Statment)
        End Sub
        Public Sub New(ByRef Nodetype As String, ByRef Program As List(Of Expression))
            MyBase.New(Nodetype)
            Statements = Program
            Dim i As String = ""
            For Each item In Statements
                i &= item.GetExpr & vbNewLine
            Next
            Expr = i
        End Sub

    End Class

End Namespace

Namespace ConcreteExpressions

    Public Class ConditionalOperation
        Inherits BinaryExpression
        Private Env As ZX81_RAM
        Public Sub New(ByRef Left As ConstantExpression, iOperator As String, Right As ConstantExpression, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("_OPERATION", "_CONDITIONAL_OPERATION", "", "BOOLEAN", ParentEnv)
            Expr = Left.GetExpr & iOperator & Right.GetExpr
            Env = ParentEnv
        End Sub
        Public Overrides Function GetValue() As String
            Evaluate(Env)
            Return iValue
        End Function
        Public Overrides Function Evaluate(ByRef ParentEnv As ZX81_RAM) As String
            iValue = EvaluateBoolean(Left, iOperator, Right)
            Return GetValue()
        End Function
        ''' <summary>
        ''' Evaluate node values ( imeadiatly invoked expression )
        ''' </summary>
        ''' <param name="Left"></param>
        ''' <param name="iOperator"></param>
        ''' <param name="Right"></param>
        ''' <returns></returns>
        Private Function EvaluateBoolean(ByRef Left As ConstantExpression, ByRef iOperator As String, ByRef Right As ConstantExpression) As String

            If Left.VarType = "INT" And Right.VarType = "INT" Then
                Select Case iOperator
                    Case ">="
                        Return (Integer.Parse(Left.GetValue) >= Integer.Parse(Left.GetValue)).ToString
                    Case "<="
                        Return (Integer.Parse(Left.GetValue) <= Integer.Parse(Left.GetValue)).ToString
                    Case ">"
                        Return (Integer.Parse(Left.GetValue) > Integer.Parse(Left.GetValue)).ToString
                    Case "<"
                        Return (Integer.Parse(Left.GetValue) < Integer.Parse(Left.GetValue)).ToString
                    Case "="
                        Return (Integer.Parse(Left.GetValue) = Integer.Parse(Left.GetValue)).ToString

                End Select


            End If
            Return False.ToString
        End Function
    End Class
    Public Class AddativeOperation
        Inherits BinaryExpression
        Private Env As ZX81_RAM
        Public Sub New(ByRef Left As ConstantExpression, iOperator As String, Right As ConstantExpression, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("_OPERATION", "ADDATIVE_OPERATION", "", "INT", ParentEnv)
            Expr = (Left.GetExpr & iOperator & Right.GetExpr)
        End Sub
        Public Overrides Function GetValue() As String
            Evaluate(Env)
            Return iValue
        End Function
        Public Overrides Function Evaluate(ByRef ParentEnv As ZX81_RAM) As String
            iValue = EvaluateAddative(Left, iOperator, Right)

            Return GetValue()
        End Function


        ''' <summary>
        ''' Enables for evaluation of the node / Imediatly invoked expression
        ''' </summary>
        ''' <param name="Left"></param>
        ''' <param name="iOperator"></param>
        ''' <param name="Right"></param>
        ''' <returns></returns>
        Private Function EvaluateAddative(ByRef Left As ConstantExpression, ByRef iOperator As String, ByRef Right As ConstantExpression) As String

            If Left.VarType = "INT" And Right.VarType = "INT" Then
                Select Case iOperator
                    Case "+"
                        Return (Integer.Parse(Left.GetValue) + Integer.Parse(Right.GetValue)).ToString
                    Case "-"
                        Return (Integer.Parse(Left.GetValue) - Integer.Parse(Right.GetValue)).ToString
                End Select


            End If
            Return 0
        End Function
    End Class
    Public Class MultiplicativeOperation
        Inherits BinaryExpression
        Private env As ZX81_RAM
        Public Sub New(ByRef Left As ConstantExpression, iOperator As String, Right As ConstantExpression, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("_OPERATION", "MULTIPLICAIVE_OPERATION", "", "INT", ParentEnv)
            Expr = (Left.GetExpr & iOperator & Right.GetExpr)
        End Sub

        Public Overrides Function GetValue() As String
            Evaluate(env)
            Return iValue
        End Function
        Public Overrides Function Evaluate(ByRef ParentEnv As ZX81_RAM) As String
            iValue = EvaluateMultiplicative(Left, iOperator, Right)
            Return GetValue()
        End Function
        ''' <summary>
        ''' Allows for evaluation of the node : Imeadialty invoked expression
        ''' </summary>
        ''' <param name="Left"></param>
        ''' <param name="iOperator"></param>
        ''' <param name="Right"></param>
        ''' <returns></returns>
        Private Function EvaluateMultiplicative(ByRef Left As ConstantExpression, ByRef iOperator As String, ByRef Right As ConstantExpression) As String

            If Left.VarType = "INT" And Right.VarType = "INT" Then
                Select Case iOperator
                    Case "*"
                        Return (Integer.Parse(Left.GetValue) * Integer.Parse(Right.GetValue)).ToString
                    Case "/"
                        Return (Integer.Parse(Left.GetValue) / Integer.Parse(Right.GetValue)).ToString
                End Select


            End If
            Return 0
        End Function
    End Class
    Public Class PrintFunction
        Inherits Expression
        Public ToPrint As UnaryExpression
        Public ParentEnv As ZX81_RAM
        Public Sub New(ByRef ToPrint As UnaryExpression, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("PRINT_FUNCTION")
            Me.ToPrint = ToPrint
            Expr = "PRINT" & ToPrint.GetValue
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
        End Sub
        Public Sub New(ByRef ToPrint As ConstantExpression, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("PrintFunction")
            Me.ToPrint.VarType = ToPrint.VarType
            Me.ToPrint.Expr = ToPrint.Expr
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
            Expr = "PRINT" & ToPrint.GetValue
        End Sub
        Public Sub New(ByRef ToPrint As BinaryExpression)
            MyBase.New("PrintFunction")
            Me.ToPrint.Expr = ToPrint.Expr
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
            Expr = "PRINT" & ToPrint.GetValue
        End Sub
        Public Sub New(ByRef ToPrint As ConditionalOperation, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("PrintFunction")
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
            Me.ToPrint.Expr = ToPrint.Expr

            Expr = "PRINT" & ToPrint.GetValue
        End Sub
        Public Sub New(ByRef ToPrint As MultiplicativeOperation, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("PrintFunction")

            Me.ToPrint.Expr = ToPrint.Expr
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
            Expr = "PRINT" & ToPrint.GetValue
        End Sub
        Public Sub New(ByRef ToPrint As AddativeOperation, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("PrintFunction")

            Me.ToPrint.Expr = ToPrint.Expr
            Me.ParentEnv = ParentEnv
            Me.ToPrint.iValue = ToPrint.Evaluate(ParentEnv)
            Expr = "PRINT" & ToPrint.GetValue
        End Sub
        Public Overrides Function Evaluate(ByRef ParentEnV As ZX81_RAM) As String
            Return ToPrint.GetValue
        End Function
    End Class

    Public Class IfFunction
        Inherits UnaryExpression
        Public Statements As Body
        Public Conditional As ConditionalOperation
        Public Env As ZX81_RAM

        Public Sub New(ByRef Condition As ConditionalOperation, ByRef Block As Body, ByRef ParentEnv As ZX81_RAM)
            MyBase.New("_IF_FUNCTION", "IF_TRUE", Condition.GetValue, "BOOLEAN", ParentEnv)
            Me.ParentEnv = ParentEnv
            Conditional = Condition
            Statements = Block
        End Sub
        Public Overrides Function GetValue() As String
            Return Conditional.GetValue
        End Function

        Public Overrides Function Evaluate(ByRef ParentEnv As ZX81_RAM) As String
            Dim i As String = ""
            If Conditional.Evaluate(ParentEnv) = "TRUE" Then
                For Each item In Statements.Statements
                    i &= item.Evaluate(ParentEnv) * vbNewLine
                Next
            End If

            Return GetValue()
        End Function
    End Class
    Public Class LoopFunction
        Inherits Expression
        Public body As Body
        Public Sub New(ByRef Body As Body, ByRef ParentEnvironment As ZX81_RAM)
            MyBase.New("_LOOP_FUNCTION")
        End Sub

        Public Overrides Function Evaluate(ByRef ParentEnvironment As ZX81_RAM) As String
            For Each item In body.Statements
                item.Evaluate(ParentEnvironment)
            Next
            Return "TRUE"
        End Function
    End Class
End Namespace


