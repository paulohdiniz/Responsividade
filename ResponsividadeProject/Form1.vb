Public Class Form1
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim path As String
        Dim text As String
        Dim indice As Integer
        Dim temporario As Double
        Dim yInterpolado(0) As Double

        Dim potencia(0) As Double
        Dim responsividade(0) As Double


        Dim columnXRef() As Double
        Dim columnYRef() As Double

        Dim columnXCalib() As Double
        Dim columnYCalib() As Double

        Dim columnXAmostra() As Double
        Dim columnYAmostra() As Double


        path = openFile()
        text = readTxtComplete(path)
        columnYRef = getColumYOfStringComplete(text) 'column y é a primeira coluna do txt
        columnXRef = getColumXOfStringComplete(text)  'column x é a segunda coluna do txt

        path = openFile()
        text = readTxtComplete(path)
        columnYCalib = getColumYOfStringComplete(text)
        columnXCalib = getColumXOfStringComplete(text)

        path = openFile()
        text = readTxtComplete(path)
        columnYAmostra = getColumYOfStringComplete(text)
        columnXAmostra = getColumXOfStringComplete(text)

        For i = LBound(columnXCalib) + 1 To UBound(columnXCalib)
            indice = menorque(columnXCalib(i), columnXRef)

            If (indice = -1) Then
                temporario = columnXRef(columnXRef.Length - 1)
                Add(Of Double)(yInterpolado, temporario)

            Else
                temporario = ((columnXCalib(i)) * (columnYRef(indice + 1) - columnYRef(indice)) + columnYRef(indice) * columnXRef(indice + 1) - columnXRef(indice) * columnYRef(indice + 1)) / (columnXRef(indice + 1) - columnXRef(indice))
                Add(Of Double)(yInterpolado, temporario)
            End If
            Add(Of Double)(potencia, columnYCalib(i) / temporario)
            Add(Of Double)(responsividade, (columnYAmostra(i) * temporario) / columnYCalib(i))

        Next i
        Console.WriteLine(columnXRef.Length)
        Console.WriteLine(columnYRef.Length)
        Console.WriteLine(columnXCalib.Length)
        Console.WriteLine(columnYCalib.Length)
        Console.WriteLine(columnXAmostra.Length)
        Console.WriteLine(columnYAmostra.Length)
        Console.WriteLine(potencia.Length)
        Console.WriteLine(responsividade.Length)
        Console.WriteLine(LBound(responsividade))
        Console.WriteLine(UBound(responsividade))
        For i = 0 To 89
            Console.WriteLine(responsividade(i))
        Next i














        Dim outFile As IO.StreamWriter
        Dim qlqr As String
        outFile = IO.File.AppendText("C:\Users\phdin\Documents\text.txt")
        For i = LBound(responsividade) + 1 To responsividade.Length - 1
            qlqr = responsividade(i).ToString + " " + columnXAmostra(i).ToString
            outFile.WriteLine(qlqr)
            ' outFile.WriteLine(columnXAmostra(i))

        Next i
        outFile.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'setup the chart area 
        Chart1.Titles.Add("Gráfico") 'specify chart name
        Chart1.ChartAreas.Clear()
        Chart1.ChartAreas.Add("Default")
        With Chart1.ChartAreas("Default")
            .AxisX.Title = "Comprimento de onda (nm)" 'x label
            .AxisX.MajorGrid.LineColor = Color.SkyBlue
            .AxisY.MajorGrid.LineColor = Color.SkyBlue
            .AxisY.Title = "Responsividade" 'y label
        End With
        'specify series plot lines
        Chart1.Series.Clear()
        Chart1.Series.Add("plot1")
        Chart1.Series("plot1").Color = Color.Red
        Chart1.Series("plot1").ChartType = DataVisualization.Charting.SeriesChartType.Line

        Dim path As String = openFile()
        Text = readTxtComplete(path)
        Dim y() As Double = getColumYOfStringComplete(Text)
        Dim x() As Double = getColumXOfStringComplete(Text)

        For i = 2 To x.Length - 1
            Chart1.Series("plot1").Points.AddXY(x(i), y(i))
        Next i
    End Sub
End Class
