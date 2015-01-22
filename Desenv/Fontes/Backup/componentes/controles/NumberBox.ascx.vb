<ValidationProperty("Text")> Partial Public Class NumberBox
    Inherits System.Web.UI.UserControl
    Public Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Property Text() As String
        Get
            Return txtNumberBox.Text
        End Get
        Set(ByVal value As String)
            txtNumberBox.Text = value
        End Set
    End Property
    Public Property ValidChars() As String
        Get
            Return txtNumberBox_FilteredTextBoxExtender.ValidChars
        End Get
        Set(ByVal value As String)
            txtNumberBox_FilteredTextBoxExtender.ValidChars = value
        End Set
    End Property
    Public Property MinValue() As Double
        Get
            Return txtNumberBox_NumericUpDownExtender.Minimum
        End Get
        Set(ByVal value As Double)
            txtNumberBox_NumericUpDownExtender.Minimum = value
        End Set
    End Property
    Public Property MaxValue() As Double
        Get
            Return txtNumberBox_NumericUpDownExtender.Minimum
        End Get
        Set(ByVal value As Double)
            txtNumberBox_NumericUpDownExtender.Maximum = value

        End Set
    End Property
    Public Property EnableUpDown() As Boolean
        Get
            Return txtNumberBox_NumericUpDownExtender.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtNumberBox_NumericUpDownExtender.Enabled = value
            spanBotoes.Visible = value
        End Set
    End Property
    Public Property xWidth() As WebControls.Unit
        Get
            Return txtNumberBox.Width
        End Get
        Set(ByVal value As WebControls.Unit)
            'txtNumberBox_NumericUpDownExtender.Width = value
            txtNumberBox.Width = value
            txtNumberBox_NumericUpDownExtender.Width = Integer.Parse(value.ToString())
        End Set
    End Property
    Public Property MaxLength() As Integer
        Get
            Return txtNumberBox.MaxLength
        End Get
        Set(ByVal value As Integer)
            txtNumberBox.MaxLength = value
        End Set
    End Property
    Public Property Columns() As Integer
        Get
            Return txtNumberBox.Columns
        End Get
        Set(ByVal value As Integer)
            txtNumberBox.Columns = value
        End Set
    End Property
    Public Property CssClass() As String
        Get
            Return txtNumberBox.CssClass
        End Get
        Set(ByVal value As String)
            txtNumberBox.CssClass = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return txtNumberBox.Enabled
        End Get
        Set(ByVal value As Boolean)
            txtNumberBox.Enabled = value
        End Set
    End Property
    Private Sub txtNumberBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumberBox.TextChanged
        RaiseEvent TextChanged(sender, e)
    End Sub
    Public Property AutoPostBack() As Boolean
        Get
            Return txtNumberBox.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            txtNumberBox.AutoPostBack = value
        End Set
    End Property
    Public Overloads ReadOnly Property Attributes() As AttributeCollection
        Get
            Return txtNumberBox.Attributes
        End Get
    End Property
    Public ReadOnly Property ClientIDNumber() As String
        Get
            Return txtNumberBox.ClientID
        End Get
    End Property
    Public ReadOnly Property IsNumericValue() As Boolean
        Get
            Return IsNumeric(txtNumberBox.Text)
        End Get
    End Property
    Public ReadOnly Property Value() As Decimal
        Get
            Dim nRet As Decimal
            Decimal.TryParse(txtNumberBox.Text, nRet)
            Return nRet
        End Get
    End Property
    Public ReadOnly Property IsEmpty() As Boolean
        Get
            Return (txtNumberBox.Text.Trim.Length = 0)
        End Get
    End Property
    Public Property [ReadOnly]() As Boolean
        Get
            Return txtNumberBox.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtNumberBox.ReadOnly = value
        End Set
    End Property
End Class