
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ArrayOfCriteria
{

    private ArrayOfCriteriaCriteria[] criteriaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Criteria")]
    public ArrayOfCriteriaCriteria[] Criteria
    {
        get
        {
            return this.criteriaField;
        }
        set
        {
            this.criteriaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ArrayOfCriteriaCriteria
{

    private byte binNoField;

    private string descriptionField;

    private ArrayOfCriteriaCriteriaLabelColor labelColorField;

    /// <remarks/>
    public byte BinNo
    {
        get
        {
            return this.binNoField;
        }
        set
        {
            this.binNoField = value;
        }
    }

    /// <remarks/>
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public ArrayOfCriteriaCriteriaLabelColor LabelColor
    {
        get
        {
            return this.labelColorField;
        }
        set
        {
            this.labelColorField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ArrayOfCriteriaCriteriaLabelColor
{

    private byte aField;

    private byte rField;

    private byte gField;

    private byte bField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte A
    {
        get
        {
            return this.aField;
        }
        set
        {
            this.aField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte R
    {
        get
        {
            return this.rField;
        }
        set
        {
            this.rField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte G
    {
        get
        {
            return this.gField;
        }
        set
        {
            this.gField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte B
    {
        get
        {
            return this.bField;
        }
        set
        {
            this.bField = value;
        }
    }
}

