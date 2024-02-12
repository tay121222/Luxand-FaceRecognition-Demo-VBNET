Imports System.Data.SqlClient
Imports Luxand
Public Class Form2
    Dim con As SqlConnection
    Dim FaceListt As List(Of TTFaceRecord)

    Public Structure TTFaceRecord
        Dim Template As Byte()
        'Face Template;
        Dim FacePosition As FSDK.TFacePosition
        Dim FacialFeatures As FSDK.TPoint()
        'Facial Features;
        Dim MyID As String

        Dim imagee As FSDK.CImage
        Dim faceImage As FSDK.CImage
    End Structure

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If (FSDK.ActivateLibrary("bSB3NdbTnv/0eW/uhypSe6hDMtjZ76Sisw5NwcN+0sfahxOtoUW22el54e/M6cSG5/xsdVIorPgugbTIfoIIn7ltyw1QMSleNebVx/Xe8aRA8bP+aVDybjoWdW/0rDP9Pv7yqBzNXyuwjgsVhPB53VGP8oTirTSUP7PTzSwOEe0=") <> FSDK.FSDKE_OK) Then
            MessageBox.Show("Please run the License Key Wizard (Start - Luxand - FaceSDK - License Key Wizard)", "Error activating FaceSDK")
            Close()
        End If
        FSDK.InitializeLibrary()
        FaceListt = New List(Of TTFaceRecord)
        loadalldata()
    End Sub

    Sub loadalldata()

        FaceListt.Clear()
        Dim checkstate As String = ""

        con = New SqlConnection
        con.ConnectionString = Form1.connection
        con.Open()
        Dim sqlstr As String = "SELECT MyID, FacePositionXc, FacePositionYc, FacePositionW, FacePositionAngle, Eye1X, Eye1Y, Eye2X, Eye2Y, Template, Image, FaceImage FROM image_data"
        Dim cmd = New SqlCommand(sqlstr, con)
        Dim sqlReader As SqlDataReader = cmd.ExecuteReader
        For i As Integer = 1 To sqlReader.FieldCount


            While sqlReader.Read

                Dim fr As New TTFaceRecord()
                fr.MyID = sqlReader.GetString(0)
                fr.FacePosition = New FSDK.TFacePosition()
                fr.FacePosition.xc = sqlReader.GetInt32(1)
                fr.FacePosition.yc = sqlReader.GetInt32(2)
                fr.FacePosition.w = sqlReader.GetInt32(3)
                'fr.FacePosition.angle = sqlReader.GetFloat(4)
                fr.FacePosition.angle = sqlReader("FacePositionAngle")
                fr.FacialFeatures = New FSDK.TPoint(1) {}
                fr.FacialFeatures(0) = New FSDK.TPoint()
                fr.FacialFeatures(0).x = sqlReader.GetInt32(5)
                fr.FacialFeatures(0).y = sqlReader.GetInt32(6)
                fr.FacialFeatures(1) = New FSDK.TPoint()
                fr.FacialFeatures(1).x = sqlReader.GetInt32(7)
                fr.FacialFeatures(1).y = sqlReader.GetInt32(8)
                fr.Template = New Byte(FSDK.TemplateSize - 1) {}
                sqlReader.GetBytes(9, 0, fr.Template, 0, FSDK.TemplateSize)

                Dim img As Image = Image.FromStream(New System.IO.MemoryStream(DirectCast(sqlReader.GetValue(10), Byte())))
                Dim img_face As Image = Image.FromStream(New System.IO.MemoryStream(DirectCast(sqlReader.GetValue(11), Byte())))

                fr.imagee = New FSDK.CImage(img)
                fr.faceImage = New FSDK.CImage(img_face)


                FaceListt.Add(fr)

                img.Dispose()
                img_face.Dispose()

            End While

        Next


        con.Close()
        cmd.Dispose()
        sqlReader.Dispose()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        PictureBox3.Image = Nothing
        PictureBox4.Image = Nothing

        Dim dlg As New OpenFileDialog
        dlg.Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg"
        dlg.Multiselect = True

        Try
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then


                PictureBox1.Image = Image.FromFile(dlg.FileName)

                FSDK.SetFaceDetectionParameters(False, True, 384)
                FSDK.SetFaceDetectionThreshold(CInt(3))

                Dim fr As New TTFaceRecord()
                fr.MyID = ""
                fr.FacePosition = New FSDK.TFacePosition()
                fr.FacialFeatures = New FSDK.TPoint(1) {}
                fr.Template = New Byte(FSDK.TemplateSize - 1) {}
                'image = New FSDK.CImage(currentframe)
                fr.imagee = New FSDK.CImage(PictureBox1.Image)
                fr.FacePosition = fr.imagee.DetectFace()


                If 0 = fr.FacePosition.w Then
                    If dlg.FileNames.Length <= 1 Then
                        MessageBox.Show("No faces found", "Enrollment error")
                        Exit Sub
                    Else
                        MessageBox.Show("No faces found" & vbCr & vbLf)
                        Exit Sub
                    End If
                Else

                    fr.faceImage = fr.imagee.CopyRect(CInt(fr.FacePosition.xc - Math.Round(fr.FacePosition.w * 0.5)), CInt(fr.FacePosition.yc - Math.Round(fr.FacePosition.w * 0.5)), CInt(fr.FacePosition.xc + Math.Round(fr.FacePosition.w * 0.5)), CInt(fr.FacePosition.yc + Math.Round(fr.FacePosition.w * 0.5)))

                    Try
                        fr.FacialFeatures = fr.imagee.DetectEyesInRegion(fr.FacePosition)
                    Catch ex2 As Exception
                        MessageBox.Show(ex2.Message, "Error detecting eyes.")
                    End Try

                    Try
                        ' get template with higher precision
                        fr.Template = fr.imagee.GetFaceTemplateInRegion(fr.FacePosition)
                    Catch ex2 As Exception
                        MessageBox.Show(ex2.Message, "Error retrieving face template.")
                    End Try


                    PictureBox3.Image = (fr.faceImage.ToCLRImage())


                    findmatch(fr)

                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub findmatch(SearchFace As TTFaceRecord)
        Dim img As Image = SearchFace.imagee.ToCLRImage
        ImageList1.Images.Clear()
        '--------------------------------------------------------------------------
        Dim Threshold As Single = 0.0F
        FSDK.GetMatchingThresholdAtFAR(100 / 100, Threshold)

        Dim MatchedCount As Integer = 0
        Dim FaceCount As Integer = FaceListt.Count
        Dim Similarities As Single() = New Single(FaceCount - 1) {}
        Dim Numbers As Integer() = New Integer(FaceCount - 1) {}

        For i As Integer = 0 To FaceListt.Count - 1
            If i = FaceListt.Count Then
                Exit For
            Else

                Dim Similarity As Single = 0.0F
                Dim CurrentFace As TTFaceRecord = FaceListt(i)
                FSDK.MatchFaces(SearchFace.Template, CurrentFace.Template, Similarity)
                If Similarity >= Threshold Then
                    Similarities(MatchedCount) = Similarity
                    Numbers(MatchedCount) = i
                    MatchedCount += 1
                End If
            End If

        Next

        If MatchedCount = 0 Then
            MessageBox.Show("No matches found. You can try to increase the FAR parameter in the Options dialog box.", "No matches")
        Else
            Dim cmp As New floatReverseComparer()
            Array.Sort(Similarities, Numbers, 0, MatchedCount, DirectCast(cmp, IComparer(Of Single)))

            Try
                For i As Integer = 0 To MatchedCount - 1
                    'either to show full picture of identified image or just face of the identified
                    ImageList1.Images.Add(FaceListt(Numbers(i)).imagee.ToCLRImage())
                Next
            Catch ex As Exception
                'MsgBox(ex.ToString)
            End Try

        End If


        Me.Show()

        Try
            If (Similarities(0) * 100.0F) > 50 Then
                PictureBox4.Image = ImageList1.Images(0)
                Label1.Text = FaceListt(Numbers(0)).MyID
                GroupBox4.Text = "Best Match - " & (Similarities(0) * 100 & "%")
            ElseIf (Similarities(0) * 100.0F) < 50 Then
                PictureBox4.Image = Nothing
                Label1.Text = "No match!"
                GroupBox4.Text = "No Match - " & (Similarities(0) * 100 & "%")
            End If
        Catch ex As SqlException
            MsgBox(ex.ToString)
        End Try


    End Sub

    Public Class floatReverseComparer
        Implements System.Collections.Generic.IComparer(Of Single)


        Public Function Compare1(x As Single, y As Single) As Integer Implements System.Collections.Generic.IComparer(Of Single).Compare
            Return y.CompareTo(x)
        End Function
    End Class
End Class