﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.20915.0
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On



<System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"), _
 System.ServiceModel.ServiceContractAttribute([Namespace]:="http://Microsoft.ServiceModel.Samples", ConfigurationName:="ICalculator", SessionMode:=System.ServiceModel.SessionMode.Required)> _
Public Interface ICalculator

    <System.ServiceModel.OperationContractAttribute(Action:="http://Microsoft.ServiceModel.Samples/ICalculator/Add", ReplyAction:="http://Microsoft.ServiceModel.Samples/ICalculator/AddResponse"), _
     System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)> _
    Function Add(ByVal n As Double) As Double

    <System.ServiceModel.OperationContractAttribute(Action:="http://Microsoft.ServiceModel.Samples/ICalculator/Subtract", ReplyAction:="http://Microsoft.ServiceModel.Samples/ICalculator/SubtractResponse"), _
     System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)> _
    Function Subtract(ByVal n As Double) As Double

    <System.ServiceModel.OperationContractAttribute(Action:="http://Microsoft.ServiceModel.Samples/ICalculator/Multiply", ReplyAction:="http://Microsoft.ServiceModel.Samples/ICalculator/MultiplyResponse"), _
     System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)> _
    Function Multiply(ByVal n As Double) As Double

    <System.ServiceModel.OperationContractAttribute(Action:="http://Microsoft.ServiceModel.Samples/ICalculator/Divide", ReplyAction:="http://Microsoft.ServiceModel.Samples/ICalculator/DivideResponse"), _
     System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)> _
    Function Divide(ByVal n As Double) As Double
End Interface

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
Public Interface ICalculatorChannel
    Inherits ICalculator, System.ServiceModel.IClientChannel
End Interface

<System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
Partial Public Class CalculatorClient
    Inherits System.ServiceModel.ClientBase(Of ICalculator)
    Implements ICalculator

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String)
        MyBase.New(endpointConfigurationName)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(binding, remoteAddress)
    End Sub

    Public Function Add(ByVal n As Double) As Double Implements ICalculator.Add
        Return MyBase.Channel.Add(n)
    End Function

    Public Function Subtract(ByVal n As Double) As Double Implements ICalculator.Subtract
        Return MyBase.Channel.Subtract(n)
    End Function

    Public Function Multiply(ByVal n As Double) As Double Implements ICalculator.Multiply
        Return MyBase.Channel.Multiply(n)
    End Function

    Public Function Divide(ByVal n As Double) As Double Implements ICalculator.Divide
        Return MyBase.Channel.Divide(n)
    End Function
End Class
