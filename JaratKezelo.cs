namespace JaratKezeloProject
{

    public class NegativKesesException : Exception
    {
        public NegativKesesException(string message) : base(message)
        {

        }
    }

    public class JaratKezelo
    {
        List<Jarat> jaratok;

        public JaratKezelo()
        {
            jaratok = new List<Jarat>();
        }

        public void UjJarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            if (jaratok.Exists(x => x.jaratSzam == jaratSzam))
            {
                throw new ArgumentException("A járat már létezik!");
            }
            jaratok.Add(new Jarat
            {
                jaratSzam = jaratSzam,
                honnanRepter = repterHonnan,
                hovaRepter = repterHova,
                indulas = indulas,
                keses = 0
            });
        }

        /// <summary>
        /// Adott járat késését növeli vagy csökkenti
        /// </summary>
        /// <param name="jaratSzam">Járat száma</param>
        /// <param name="keses">Késés percben</param>
        /// <exception cref="ArgumentException">Járat szám hibás</exception>
        /// <exception cref="NegativKesesException">Ha a késés negatívvá válna</exception>
        public void Keses(string jaratSzam, int keses)
        {
            var jarat = jaratok.Find(x => x.jaratSzam == jaratSzam);
            if (jarat == null)
            {
                throw new ArgumentException("Nincs ilyen járat!");
            }

            if (jarat.keses + keses < 0)
            {
                throw new NegativKesesException("A totál késés nem lehet negatív!");
            }

            jarat.keses += keses;
        }

        /// <summary>
        /// Megadja, hogy az adott járat mikor indul késéssel együtt
        /// </summary>
        /// <param name="jaratSzam">Járat szám</param>
        /// <returns>Tényleges indulás</returns>
        /// <exception cref="ArgumentException">Hibás járat szám</exception>
        public DateTime MikorIndul(string jaratSzam)
        {
            var jarat = jaratok.Find(x => x.jaratSzam == jaratSzam);
            if (jarat == null)
            {
                throw new ArgumentException("Nincs ilyen járat!");
            }
            return jarat.indulas.AddMinutes(jarat.keses);
        }

        /// <summary>
        /// Adott reptérről induló járatok listája
        /// </summary>
        /// <param name="repter">Reptér neve</param>
        /// <returns>Járat számok listája</returns>
        public List<string> JaratokRepuloterrol(string repter)
        {
            return jaratok.Where(x => x.honnanRepter == repter).Select(x => x.jaratSzam).ToList();
        }
    }

    public class Jarat
    {
        public required string jaratSzam;
        public required string honnanRepter;
        public required string hovaRepter;
        public DateTime indulas;
        public int keses;
    }
}
