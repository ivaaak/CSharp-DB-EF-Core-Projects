using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "User")]
    public partial class UserExportDto
    {

        private UserPurchase[] purchasesField;

        private decimal totalSpentField;

        private string usernameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Purchase", IsNullable = false)]
        public UserPurchase[] Purchases
        {
            get
            {
                return this.purchasesField;
            }
            set
            {
                this.purchasesField = value;
            }
        }

        /// <remarks/>
        public decimal TotalSpent
        {
            get
            {
                return this.totalSpentField;
            }
            set
            {
                this.totalSpentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string username
        {
            get
            {
                return this.usernameField;
            }
            set
            {
                this.usernameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class UserPurchase
    {

        private string cardField;

        private string cvcField;

        private string dateField;

        private UserPurchaseGame gameField;

        /// <remarks/>
        public string Card
        {
            get
            {
                return this.cardField;
            }
            set
            {
                this.cardField = value;
            }
        }

        /// <remarks/>
        public string Cvc
        {
            get
            {
                return this.cvcField;
            }
            set
            {
                this.cvcField = value;
            }
        }

        /// <remarks/>
        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        public UserPurchaseGame Game
        {
            get
            {
                return this.gameField;
            }
            set
            {
                this.gameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class UserPurchaseGame
    {

        private string genreField;

        private decimal priceField;

        private string titleField;

        /// <remarks/>
        public string Genre
        {
            get
            {
                return this.genreField;
            }
            set
            {
                this.genreField = value;
            }
        }

        /// <remarks/>
        public decimal Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
    }
}





