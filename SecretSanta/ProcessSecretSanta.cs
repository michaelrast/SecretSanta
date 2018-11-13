using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient;

namespace SecretSanta
{
    public class ProcessSecretSanta
    {
        private List<SkoMember> _skoMembers { get; set; }
        private string testNumber =  "yourNumberToTestWith";

        public void Process(bool production)
        {
            SendMail sendMail = new SendMail();
            List<Pairing> pairings = new List<Pairing>();
            _skoMembers = CompileSkoMembers();

            Console.WriteLine($"Found {_skoMembers.Count} members in Sko.");

            while (!PairingsValid(pairings))
            {
                pairings = FindPairings();
            }

            Console.WriteLine($"Found Successful Pairings.");
            Console.WriteLine($"Sending Progress 0%.");
            var numberThrough = 0M;
            foreach (var pairing in pairings)
            {
                //test area
                var phoneNumber = pairing.Santa.PhoneNumber;
                if (!production)
                    phoneNumber = testNumber;

                sendMail.SendToPhone(phoneNumber, "2018 Secret Santa", $"You {pairing.Santa.Name} are {pairing.Receiver.Name}s Secret Santa! Target $25 gift.");
                numberThrough++;
                decimal percentage = numberThrough / pairings.Count;
                Console.WriteLine($"Sending Progress {percentage}%.");
            }

            Console.WriteLine($"Successfully sent all messages.");
            Console.ReadKey();
        }
        
        private List<Pairing> FindPairings()
        {
            List<Pairing> returnList = new List<Pairing>();

            Random rnd = new Random();

            foreach (var skoMember in _skoMembers)
            {
                var partnerOptions = _skoMembers.Where(i => i.Name != skoMember.Name && i.Picked == false).ToList();

                if (partnerOptions.Count == 0)
                    continue;

                var randomNumber = rnd.Next(0, partnerOptions.Count - 1);
                var partner = partnerOptions[randomNumber];
                partner.Picked = true;
                returnList.Add(new Pairing(skoMember, partner));
            }

            return returnList;
        }

        private bool PairingsValid(List<Pairing> pairings)
        {
            if (pairings.Count != _skoMembers.Count)
            {
                ResetPicked();
                return false;
            }

            foreach (var pairing in pairings)
            {
                if (pairing.Santa.Name == pairing.Receiver.Name)
                {
                    ResetPicked();
                    return false;
                }
            }

            return true;
        }
        private void ResetPicked()
        {
            foreach (var skoMember in _skoMembers)
                skoMember.Picked = false;
        }

        private List<SkoMember> CompileSkoMembers()
        {
            List<SkoMember> returnList = new List<SkoMember>();

            returnList.Add(new SkoMember("User1", "PhoneNumber1"));
            returnList.Add(new SkoMember("User2", "PhoneNumber2"));
            returnList.Add(new SkoMember("User3", "PhoneNumber3"));

            return returnList;
        }
    }

    public class SkoMember
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool Picked { get; set; }

        public SkoMember(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Picked = false;
        }
    }

    public class Pairing
    {
        public SkoMember Santa { get; set; }
        public SkoMember Receiver { get; set; }


        public Pairing(SkoMember santa, SkoMember receiver)
        {
            Santa = santa;
            Receiver = receiver;
        }
    }
}
