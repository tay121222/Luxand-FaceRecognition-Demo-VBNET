Imports System.IO
Imports System.Data.SqlClient
Imports Luxand
Imports Emgu.CV
Imports Emgu.CV.Structure
Public Class Form1
    Dim sql As String
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Private WithEvents cam As New DSCamCapture
    Public connection As String = String.Format("Server=.;Database=image_face_reg;User Id=techdinos;Password=techdinos;Trusted_Connection=True")


    Dim Template As Byte()

    Dim FacePosition As FSDK.TFacePosition
    Dim FacialFeatures As FSDK.TPoint()
    Dim imagee As FSDK.CImage
    Dim faceImage As FSDK.CImage


    Dim imagefilename As String
    Dim vidCapture As Capture
    Dim imgFrame As Image(Of Bgr, Byte)
    Dim framesavedcurrent As String
    Public Event FrameSaved(ByVal capImage As Bitmap, ByVal imgPath As String)
    Dim MyPicturesFolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)) & "\Temp_Pictures"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'API Initialization
        If (FSDK.ActivateLibrary("bSB3NdbTnv/0eW/uhypSe6hDMtjZ76Sisw5NwcN+0sfahxOtoUW22el54e/M6cSG5/xsdVIorPgugbTIfoIIn7ltyw1QMSleNebVx/Xe8aRA8bP+aVDybjoWdW/0rDP9Pv7yqBzNXyuwjgsVhPB53VGP8oTirTSUP7PTzSwOEe0=") <> FSDK.FSDKE_OK) Then
            MessageBox.Show("Please run the License Key Wizard (Start - Luxand - FaceSDK - License Key Wizard)", "Error activating FaceSDK")
            Close()
        Else
            FSDK.InitializeLibrary()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles browse_img.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg"
        dlg.Multiselect = True

        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

            FSDK.SetFaceDetectionParameters(False, True, 384)
            FSDK.SetFaceDetectionThreshold(CInt(3))

            For Each fn As String In dlg.FileNames

                PictureBox1.Image = Image.FromFile(dlg.FileName)
                PictureBox2.Image = Image.FromFile(dlg.FileName)

                Dim nm As String = InputBox("Please enter name of uploaded Picture", "Enter name", "", , )

                If nm = "" Or String.IsNullOrEmpty(nm) Then
                    nm = "Unknown"
                End If

                imagefilename = nm

                'Feature Parameters declarations
                FacePosition = New FSDK.TFacePosition()
                FacialFeatures = New FSDK.TPoint(1) {}
                Template = New Byte(FSDK.TemplateSize - 1) {}
                imagee = New FSDK.CImage(PictureBox2.Image)
                FacePosition = imagee.DetectFace()


                'Feature Extraction
                If 0 = FacePosition.w Then
                    If dlg.FileNames.Length <= 1 Then
                        MessageBox.Show("No faces found", "Enrollment error")
                        Exit Sub
                    Else
                        MessageBox.Show("No faces found" & vbCr & vbLf)
                        Exit Sub
                    End If
                Else

                    faceImage = imagee.CopyRect(CInt(FacePosition.xc - Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.yc - Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.xc + Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.yc + Math.Round(FacePosition.w * 0.5)))

                    Try
                        FacialFeatures = imagee.DetectEyesInRegion(FacePosition)
                    Catch ex2 As Exception
                        MessageBox.Show(ex2.Message, "Error detecting eyes.")
                    End Try

                    Try
                        ' get template with higher precision
                        Template = imagee.GetFaceTemplateInRegion(FacePosition)
                    Catch ex2 As Exception
                        MessageBox.Show(ex2.Message, "Error retrieving face template.")
                    End Try

                End If

                savetodb()
            Next
        End If
    End Sub

    Sub savetodb()
        Dim img As Image = Nothing
        Dim img_face As Image = Nothing
        Dim strm As New MemoryStream()
        Dim strm_face As New MemoryStream()
        img = imagee.ToCLRImage()
        img_face = faceImage.ToCLRImage()
        img.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg)
        img_face.Save(strm_face, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim img_array As Byte() = New Byte(strm.Length - 1) {}
        Dim img_face_array As Byte() = New Byte(strm_face.Length - 1) {}
        strm.Position = 0
        strm.Read(img_array, 0, img_array.Length)
        strm_face.Position = 0
        strm_face.Read(img_face_array, 0, img_face_array.Length)

        Dim iReturn As Boolean
        Dim conn As New SqlConnection
        conn.ConnectionString = (connection)

        'Dim sqlCommand As New MySqlCommand
        Using sqlCommand As New SqlCommand
            With sqlCommand
                .CommandText = "INSERT INTO image_data(MyID, FacePositionXc, FacePositionYc, FacePositionW, FacePositionAngle, Eye1X, Eye1Y, Eye2X, Eye2Y, Template, Image, FaceImage) " _
             & " values(@MyID, @FacePositionXc, @FacePositionYc, @FacePositionW, @FacePositionAngle, @Eye1X, @Eye1Y, @Eye2X, @Eye2Y, @Template, @Image, @FaceImage)"
                .Connection = conn
                .CommandType = CommandType.Text

                .Parameters.AddWithValue("@MyID", imagefilename)
                .Parameters.AddWithValue("@FacePositionXc", FacePosition.xc)
                .Parameters.AddWithValue("@FacePositionYc", FacePosition.yc)
                .Parameters.AddWithValue("@FacePositionW", FacePosition.w)
                .Parameters.AddWithValue("@FacePositionAngle", FacePosition.angle)
                .Parameters.AddWithValue("@Eye1X", FacialFeatures(0).x)
                .Parameters.AddWithValue("@Eye1Y", FacialFeatures(0).y)
                .Parameters.AddWithValue("@Eye2X", FacialFeatures(1).x)
                .Parameters.AddWithValue("@Eye2Y", FacialFeatures(1).y)
                .Parameters.AddWithValue("@Template", Template)
                .Parameters.AddWithValue("@Image", img_array)
                .Parameters.AddWithValue("@FaceImage", img_face_array)

            End With
            Try
                conn.Open()
                sqlCommand.ExecuteNonQuery()
                iReturn = True
                MsgBox(imagefilename & " " & "has been captured successfully", MsgBoxStyle.Information)

            Catch ex As SqlException
                MsgBox(ex.Message.ToString)
                iReturn = False
            Finally
                conn.Close()
                img.Dispose()
                img_face.Dispose()

            End Try
        End Using
    End Sub

    Sub loadgrid()
        DataGridView1.Rows.Clear()
        Dim con As New SqlConnection
        con.ConnectionString = (connection)
        sql = " select MyID,image From image_data"
        cmd = New SqlCommand(sql, con)
        adp = New SqlDataAdapter(cmd)

        Dim ds1 As New Data.DataTable()

        con.Open()

        adp.SelectCommand.ExecuteNonQuery()

        adp.Fill(ds1)

        con.Close()


        For i = 0 To ds1.Rows.Count - 1
            DataGridView1.Rows.Add()
            Dim ImgStream As New IO.MemoryStream(CType(ds1.Rows(i)("image"), Byte()))
            With DataGridView1
                .Rows(i).Cells(0).Value = (ds1.Rows(i)("MyID"))
                .Rows(i).Cells(1).Value = Image.FromStream(ImgStream)
                .AutoResizeRows()
                .AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            End With
            ImgStream.Dispose()
        Next
        DataGridView1.AutoResizeRows()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles load_data.Click
        loadgrid()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = Me.DataGridView1.Rows(e.RowIndex)
            Try

                PB_Gallery1.BackgroundImage = row.Cells(1).Value

            Catch ex As SqlException
                MsgBox(ex.ToString)
                PB_Gallery1.BackgroundImage = Nothing
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Form2.Close()
        Form2.ShowDialog()
    End Sub

    Sub cameralistpopulation()
        ComboBoxcameralist.Items.AddRange(cam.GetCaptureDevices)
        If ComboBoxcameralist.Items.Count > 0 Then ComboBoxcameralist.SelectedIndex = 0

        For Each sz As String In [Enum].GetNames(GetType(DSCamCapture.FrameSizes))
            ComboBox14.Items.Add(sz.Replace("s", ""))
        Next
        If ComboBox14.Items.Count > 2 Then ComboBox14.SelectedIndex = 2
        Button5.Enabled = (ComboBoxcameralist.Items.Count > 0)

    End Sub

    '0247139452 MR BEN
    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cameralistpopulation()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If ComboBoxcameralist.Items.Count > 0 Then
            If Button5.Text = "Connect" Then
                vidCapture = New Capture(ComboBoxcameralist.SelectedIndex) ' Put the proper arguments in here
                'vidCapture = New Capture("rtsp://admin:yian1234@192.168.8.112:554/videostream.cgi?rate=0")
                Button5.Text = "Disconnect"
                AddHandler Application.Idle, AddressOf AppIdle
            Else
                RemoveHandler Application.Idle, AddressOf AppIdle
                vidCapture.Dispose()
                Button5.Text = "Connect"
                getimageandsave()
            End If
        Else

        End If
    End Sub

    Public Sub AppIdle(ByVal sender As System.Object, ByVal e As System.EventArgs)
        imgFrame = vidCapture.QueryFrame
        PictureBox1.Image = vidCapture.QueryFrame.ToBitmap ' this is where captured image go.
    End Sub

    Private Sub PictureBox1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.SizeChanged
        cam.ResizeWindow(0, 0, PictureBox1.ClientSize.Width, PictureBox1.ClientSize.Height)
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        If Not IO.Directory.Exists(MyPicturesFolder) Then IO.Directory.CreateDirectory(MyPicturesFolder)
        framesavedcurrent = Now.ToString.Replace("/", "-").Replace(":", "-").Replace(" ", "_") & ".png"
        Dim SaveAs As String = IO.Path.Combine(MyPicturesFolder, framesavedcurrent)
        PictureBox1.Image.Save(SaveAs, Imaging.ImageFormat.Png)
        RaiseEvent FrameSaved(New Bitmap(PictureBox1.Image), SaveAs)
    End Sub


    Sub getimageandsave()

        Dim nm As String = InputBox("Please enter name of uploaded Picture", "Enter name", "", , )

        If nm = "" Or String.IsNullOrEmpty(nm) Then
            nm = "Unknown"
        End If

        imagefilename = nm

        FacePosition = New FSDK.TFacePosition()
        FacialFeatures = New FSDK.TPoint(1) {}
        Template = New Byte(FSDK.TemplateSize - 1) {}
        imagee = New FSDK.CImage(PictureBox1.Image)

        FacePosition = imagee.DetectFace()



        If 0 = FacePosition.w Then
            MessageBox.Show("No faces found", "Enrollment error")
            Exit Sub
        Else

        faceImage = imagee.CopyRect(CInt(FacePosition.xc - Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.yc - Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.xc + Math.Round(FacePosition.w * 0.5)), CInt(FacePosition.yc + Math.Round(FacePosition.w * 0.5)))

        Try
            FacialFeatures = imagee.DetectEyesInRegion(FacePosition)
        Catch ex2 As Exception
            MessageBox.Show(ex2.Message, "Error detecting eyes.")
        End Try

        Try
            ' get template with higher precision
            Template = imagee.GetFaceTemplateInRegion(FacePosition)
        Catch ex2 As Exception
            MessageBox.Show(ex2.Message, "Error retrieving face template.")
        End Try

        End If

        savetodb()
    End Sub
End Class
