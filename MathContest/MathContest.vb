'Jessica McArthur
'RCET0265
'Spring 2022
'Math Contest
'

Option Strict On
Public Class MathContest
    Dim correct As Integer = 0
    Dim total As Integer = 0

    'Validate Name input
    Function NameInputValid() As Boolean
        Dim isNameValid As Boolean

        Select Case NameTextBox.Text
            Case ""
                isNameValid = False
                MsgBox("Name is not valid")
                NameTextBox.Focus()
            Case Else
                isNameValid = True
        End Select
        Return isNameValid

    End Function

    'Validate Age input. Input must be between 7- 11 in integer form.
    Function AgeInputValid() As Boolean
        Dim studentAge As Integer
        Dim isAgeValid As Boolean

        Try
            studentAge = CInt(AgeTextBox.Text)
            isAgeValid = False
            If studentAge < 7 Or studentAge > 11 Then
                isAgeValid = False
                MsgBox("Age Must be between 7-11 years")
                AgeTextBox.Clear()
                AgeTextBox.Focus()
            Else
                isAgeValid = True
            End If

        Catch
            AgeTextBox.Focus()
            isAgeValid = False
            MsgBox("Age is not valid")
            AgeTextBox.Clear()
        End Try
        Return isAgeValid
    End Function

    'Validate Grade input. Must be between 1-4 in integer form.
    Function GradeInputValid() As Boolean
        Dim studentGrade As Integer
        Dim isGradeVaild As Boolean

        Try
            studentGrade = CInt(GradeTextBox.Text)
            isGradeVaild = False
            If studentGrade < 1 Or studentGrade > 4 Then
                isGradeVaild = False
                MsgBox("Grade Must be between 1-4 grades")
                GradeTextBox.Clear()
                GradeTextBox.Focus()
            Else
                isGradeVaild = True
            End If
        Catch
            GradeTextBox.Focus()
            isGradeVaild = False
            MsgBox("Grade is not valid")
            GradeTextBox.Clear()
        End Try
        Return isGradeVaild

    End Function

    'Activates validate function as text box is left.
    Private Sub NameTextBox_Leave(sender As Object, e As EventArgs) Handles NameTextBox.Leave
        NameInputValid()
    End Sub

    Private Sub AgeTextBox_Leave(sender As Object, e As EventArgs) Handles AgeTextBox.Leave
        AgeInputValid()
    End Sub

    Private Sub GradeTextBox_Leave(sender As Object, e As EventArgs) Handles GradeTextBox.Leave
        GradeInputValid()
        ValidateInput()
    End Sub

    'Enables submit and summary button when student input is all valid
    Private Sub ValidateInput()

        If GradeInputValid() Or AgeInputValid() Or NameInputValid() = True Then
            SubmitButton.Enabled = True
            SummaryButton.Enabled = True
            NumberGenerator()
            StudentAnswerTextBox.Enabled = True
        Else
            SubmitButton.Enabled = False
            SummaryButton.Enabled = False
            StudentAnswerTextBox.Enabled = False
        End If

    End Sub

    'Generate numbers for math problem
    Private Sub NumberGenerator()

        FirstNumberTextBox.Text = CStr(RandomNumberInRange(25, 0))
        SecondNumberTextBox.Text = CStr(RandomNumberInRange(25, 0))

    End Sub

    'Generators random numbers to use in math problems
    Function RandomNumberInRange(Optional max% = 10%, Optional min% = 0%) As Integer
        Dim _max% = max - min
        If _max < 0 Then
            Throw New System.ArgumentException("Maximum number must be greater than minimum number")
        End If
        Randomize(DateTime.Now.Millisecond)
        Return CInt(System.Math.Floor(Rnd() * (_max + 1))) + min
    End Function

    'Function that gives the correct answer for the math problems.
    Function CorrectAnswer() As Integer
        Dim _correctAnswer As Integer

        If AddRadioButton.Checked = True Then
            _correctAnswer = CInt(FirstNumberTextBox.Text) + CInt(SecondNumberTextBox.Text)
        ElseIf SubtractRadioButton.Checked = True Then
            _correctAnswer = CInt(FirstNumberTextBox.Text) - CInt(SecondNumberTextBox.Text)
        ElseIf MultiplyRadioButton.Checked = True Then
            _correctAnswer = CInt(FirstNumberTextBox.Text) * CInt(SecondNumberTextBox.Text)
        ElseIf DivideRadioButton.Checked = True Then
            _correctAnswer = CInt(FirstNumberTextBox.Text) \ CInt(SecondNumberTextBox.Text)
        End If
        Return _correctAnswer
    End Function


    'Validates student response. Tells student if the answers is correct or not.  
    'Student response text box clears once the submit button is pressed. 
    'All responses are counted
    Private Sub StudentResult()
        total += 1
        Try
            If CInt(StudentAnswerTextBox.Text) = CorrectAnswer() Then
                correct += 1
                MsgBox("Your Answer is Correct!!! Tres Bien")
            Else
                MsgBox($"Answer in not correct. The correct answer is {CorrectAnswer()}")

            End If
        Catch
            StudentAnswerTextBox.Focus()
            MsgBox("Answer is not valid")
        End Try
        StudentAnswerTextBox.Clear()
    End Sub

    'When summary button is pressed the number of correctly answered problems to incorrect problems is displayed
    Private Sub SummaryResult()
        MsgBox($"{NameTextBox.Text} answered {correct} of {total} trys correct")
    End Sub

    'The form is reset to its default settings when this function is called.
    Sub Reset()
        'all text boxs are cleared
        NameTextBox.Clear()
        AgeTextBox.Clear()
        GradeTextBox.Clear()
        StudentAnswerTextBox.Clear()
        FirstNumberTextBox.Clear()
        SecondNumberTextBox.Clear()
        'Count of past problems attepmted and correct are both set to 0
        correct = 0
        total = 0
        'default setting of form
        AddRadioButton.Checked = True
        StudentAnswerTextBox.Enabled = False
        SubmitButton.Enabled = False
        SummaryButton.Enabled = False

    End Sub

    'Buttons 
    'Submit Button checks the students response and counts the respones. 
    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        StudentResult()
    End Sub
    'Clear Button set form to default settings.
    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Reset()
    End Sub
    'Summary Button displays the number of correct response to total responses. 
    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        SummaryResult()
    End Sub
    'Exit Button exits the program
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    'Starts program at default settings. 
    Private Sub MathContest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub

    'Generators random number every time a new math problem is selected (a radio button is selected)
    Private Sub AddRadioButton_CheckedChanged(sender As Object, e As EventArgs) _
        Handles AddRadioButton.CheckedChanged, SubtractRadioButton.CheckedChanged, MultiplyRadioButton.CheckedChanged, DivideRadioButton.CheckedChanged
        NumberGenerator()
        StudentAnswerTextBox.Text = ""
    End Sub
End Class
