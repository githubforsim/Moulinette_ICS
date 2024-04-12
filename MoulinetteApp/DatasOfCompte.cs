using System;

namespace NMoulinetteApp
{
    internal class DatasOfCompte
    {
        public string compte;
        public string compteLibellé;

        public DatasOfCompte(string compte, string compteLibellé)
        {
            this.compte = compte ?? throw new ArgumentNullException(nameof(compte));
            this.compteLibellé = compteLibellé ?? throw new ArgumentNullException(nameof(compteLibellé));
        }
    }
}
