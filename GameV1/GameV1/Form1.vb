Public Class Form1
    Dim r As New Random
    Dim score As Integer

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        chase(bad_guy)
    End Sub
    Sub move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)
        If Collision(p, "wall") Then
            MsgBox("jump")
        End If

    End Sub
    Sub Randmove(p As PictureBox)
        Dim x As Integer
        Dim y As Integer
        x = r.Next(-10, 11)
        y = r.Next(-10, 11)
        MoveTo(p, x, y)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                PictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
                Me.Refresh()
            Case Keys.W
                MoveTo(PictureBox2, 0, -5)
            Case Keys.S
                MoveTo(PictureBox2, 0, 5)
            Case Keys.A
                MoveTo(PictureBox2, -5, 0)
            Case Keys.D
                MoveTo(PictureBox2, 5, 0)
            Case Keys.Space
                bullet.Location = PictureBox2.Location
                bullet.Visible = True
                Timer2.Enabled = True


        End Select
    End Sub
    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(PictureBox2.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > PictureBox2.Location.X Then
            x = -2
        Else
            x = 2
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < PictureBox2.Location.Y Then
            y = 2
        Else
            y = -2
        End If
        MoveTo(p, x, y)

    End Sub



    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col

    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function

    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If p.Name = "PictureBox1" And Collision(p, "WIN", other) Then
            Me.BackColor = Color.Green
            other.visible = False
            Me.Visible = False
            Dim f As New Form2
            f.ShowDialog()
            Me.Visible = True
            Return


        End If


        If p.Name = "PictureBox2" And Collision(p, "WIN") Then
            Me.BackColor = Color.Black
            win_screen.Visible = True
            Me.Visible = False
            Dim f As New Form2
            f.ShowDialog()
            Me.Visible = True
            Return
        End If
        If p.Name = "bullet" And Collision(p, "bad_guy") Then
            bad_guy.Visible = False
            bullet.Visible = False
            PictureBox15wall.Visible = False
            Return
        End If
        If p.Name = "bullet" And Collision(p, "picturebox1") Then
            PictureBox22wall.Visible = False
        End If

        If p.Name = "PictureBox2" And Collision(p, "PictureBox4") Then
            PictureBox30wall.Visible = False
        End If

        If p.Name = "PictureBox2" And Collision(p, "bad_guy") Then
            PictureBox2.Visible = False
            Me.BackColor = Color.Black
            lose_screen.Visible = True
        End If

        If p.Name = "PictureBox2" And Collision(p, "spikes") Then
            PictureBox2.Visible = False
            Me.BackColor = Color.Black
            lose_screen.Visible = True

        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        MoveTo(bullet, 10, 0)

    End Sub


End Class
