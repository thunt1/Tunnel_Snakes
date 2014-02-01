Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            Label1.Text = "Hello World!"
        Else
            Label1.Text = TextBox1.Text
        End If
        MsgBox("Bob is boss")
    End Sub

    Private Sub AssembleRunWDebuggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssembleRunWDebuggingToolStripMenuItem.Click

    End Sub
End Class
