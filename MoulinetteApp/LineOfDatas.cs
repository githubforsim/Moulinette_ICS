namespace NMoulinetteApp
{
    class LineOfDatas
    {
        public string numéroDEcriture; // n° de la facture
        public string compte;
        public string compteLibellé; // Récupérer le libellé correspondant au compte dans le fichier comptes.txt
        public string dateDePièce;
        public string journal = "VT"; // "VT" systématiquement
        public string référence; // n° de la facture
        public string ecritureLibellé;
        public string débit;
        public string crédit;
        public string lettre = string.Empty; // Laisser vide
    }
}
