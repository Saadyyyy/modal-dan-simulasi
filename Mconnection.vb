Imports System.Data.OleDb

Module MKoneksi
    Public conn As OleDbconnection
    Public ds As DataSet
    Public da As OleDbDataAdapter
    Public str As String
    Public cmd As OleDbCommand
    Public rd As OleDbDataReader


    Public Sub koneksi()
        str = "provider=Microsoft.Jet.OLEDB.4.Data Source=db_antrian.mdb"
        conn = New OleDbconnection(str)
        If conn.state = ConnectionState.Closed Then conn.open()
    End Sub

    Sub Register()
        Call koneksi()
        Dim iduser As String
        cmd = New OleDbCommand("Select * from tb_user order by iduser desc", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            iduser = "USER" + "000"
        Else
            iduser = Val(Microsoft.VisualBasic.Mid)(rd.Item("iduser").ToString, 3, 2,)) + 1
            If Len(iduser) = 1 Then
                iduser = "USER00" & iduser & ""
            ElseIf Len(iduser) = 2 Then
                iduser = "USER0" & iduser & ""
            End If
        End If

        Dim register As String = "insert into tb_user values " & _
            "('" & iduser & "','" & _
            Fregister.tb_fullname.Text & "','" & _
            "Active" & "','" & _
            "Customer Service" & "','" & _
            Fregister.tb_username.Text & "','" & _
            Fregister.tb_password.Text & "') "
        cmd = New OleDbCommand(register, conn)
        cmd.ExecuteNonQuery()

        Flogin, Show()
        Fregister.Close()
    End Sub

    Sub Login()
        Call koneksi()
        cmd = New OleDbCommand("select * from tb_user where usernamen ='" & _
        Flogin.txt_username.Text & _
        "' and pass ='" & Flogin.txt_password.Text & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            MsgBox("Username dan Password tidak dikenali", MsgBoxStyle.Critical, _
            "Error")
        Else
            MsgBox("Login Berhasil", MsgBoxStyle.Information, "Success")

        End If
    End Sub

    Sub NomorAntrian()
        Call koneksi()
        Dim idantrian As String

        Dim j As String
        Dim nomor As Integer
        j = " Select count(*) from tb_antrian"
        cmd = New OleDb.OleDbCommand(j, conn)
        Dim n As Integer
        n = cmd.ExecuteScalar
        nomor = n
        If nomor = 0 Then
            idantrian = "A0001"
            FNomor.lb_nomor.Text = "00" & nomor + 1
        ElseIf nomor < 10 Then
            idantrian = "A000" & nomor + 1
            FNomor.lb_nomor.Text = "00" & nomor + 1
        ElseIf nomor < 100 Then
            idantrian = "A00" & nomor + 1
            FNomor.lb_nomor.Text = "0" & nomor + 1
        ElseIf nomor < 1000 Then
            idantrian = "A" & nomor + 1
        End If

        Dim nomorAntrian As String = "insert into tb_antrian values " & _
            "('" & idantrian & "','" & _
            "" & "','" & _
            "Waiting" & "','" & _
            "28/11/2022" & "','" & _
            "USER000" & "') "
        cmd = New OleDbCommand(nomorAntrian, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Terima Kasih, Anda sudah berhasil mengambil nomor antrian", _
               MsgBoxStyle.Information, "Sukses")
    End Sub

    Sub dataAntrian()
        Call koneksi()
        da = New OleDbDataAdapter("select idantrian as [NOMOR ANTRIAN], loket as ")
    End Sub







End Module
