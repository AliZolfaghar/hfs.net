Imports System.Data
Imports Microsoft.VisualBasic

'-> AZ DB Layer Wrapper (ver : 2.0.2)
'-> log data into /App_Data/DB_log.TXT
Public Class AZDBL : Implements IDisposable

    Public SqlStr As String
    Public Params As New Dictionary(Of String, String)

    Dim oCnn As New System.Data.SqlClient.SqlConnection(CnnStr)
    Dim oCmd As New System.Data.SqlClient.SqlCommand

    Public Function CnnStr() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("CnnStr").ConnectionString.ToString
    End Function

    Sub save_log(sLog As String)
        System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("/App_Data/DB_LOG.TXT"), sLog + vbCrLf)
    End Sub

    Public Function ExecuteScalar() As String
        oCnn.Open()
        oCmd.Connection = oCnn
        oCmd.CommandText = SqlStr
        oCmd.Parameters.Clear()

        For Each param In Params
            oCmd.Parameters.AddWithValue(param.Key, param.Value)
        Next
        Dim RV As String = ""
        Try
            RV = oCmd.ExecuteScalar
        Catch ex As Exception
            RV = ex.Message & " " & SqlStr
        End Try
        oCnn.Close()
        Return RV

    End Function


    Public Function ExecuteDatatable() As System.Data.DataTable
        Dim DA As New System.Data.SqlClient.SqlDataAdapter(SqlStr, CnnStr)
        DA.SelectCommand.Parameters.Clear()

        For Each param In Params
            DA.SelectCommand.Parameters.AddWithValue(param.Key, param.Value)
        Next

        Dim DT As New System.Data.DataTable

        Try
            DA.Fill(DT)

        Catch ex As Exception
            HttpContext.Current.Response.Write(SqlStr)
            HttpContext.Current.Response.Write(ex.Message)
        End Try


        DA.Dispose()

        Return DT

        DT.Dispose()

    End Function

    Public Function ExecuteNonQuery() As String
        oCnn.Open()
        oCmd.Connection = oCnn
        oCmd.CommandText = SqlStr
        oCmd.Parameters.Clear()

        For Each param In Params
            oCmd.Parameters.AddWithValue(param.Key, param.Value)
        Next
        Dim RV As String = ""
        Try
            RV = oCmd.ExecuteNonQuery
            save_log(RV)
        Catch ex As Exception

            RV = ex.Message & " " & SqlStr
            save_log(RV)
        End Try
        oCnn.Close()
        Return RV

    End Function


    Public Function ExecuteSP(SP_NAME As String) As DataSet
        Dim DS As New DataSet
        oCnn.Open()

        oCmd.Connection = oCnn
        oCmd.CommandType = Data.CommandType.StoredProcedure
        oCmd.CommandText = SP_NAME
        oCmd.Parameters.Clear()

        For Each param In Params
            oCmd.Parameters.AddWithValue(param.Key, param.Value)
        Next

        Dim DR As System.Data.SqlClient.SqlDataReader = oCmd.ExecuteReader()

        '-> loop in result-sets 
        Do
            Dim DT As New System.Data.DataTable '-> add each result-set to a data-table 

            '-> add result-set fields to data-table 
            For i = 0 To DR.FieldCount - 1
                Dim c As New System.Data.DataColumn
                c.ColumnName = DR.GetName(i)
                DT.Columns.Add(c)
            Next

            '-> read data-reader and put each row into data-table 
            While DR.Read
                Dim row As System.Data.DataRow = DT.Rows.Add '-> add a data-row to data-table 
                For i = 0 To DR.FieldCount - 1 '-> loop in data-reader fields 
                    row.Item(i) = DR.Item(i) '-> add field to data-row 
                Next
            End While

            DS.Tables.Add(DT) '-> add data-table to data-set 
        Loop While DR.NextResult

        oCmd.Dispose()
        oCnn.Close()
        oCnn.Dispose()

        Return DS
    End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                oCmd.Dispose()
                oCnn.Dispose()

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        GC.SuppressFinalize(Me)
    End Sub
#End Region



End Class




