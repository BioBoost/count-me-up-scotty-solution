using CountMeUpScottyLibrary;

namespace CountMeUpScottyGUI
{
    class Settings
    {
        public bool PlaySounds { get; set; }
        public int NumberOfChallenges { get; set; }
        public Difficulty Difficulty { get; set; }

        public void Load()
        {
            PlaySounds = Properties.Settings.Default.PlaySounds;
            NumberOfChallenges = Properties.Settings.Default.NumberOfChallenges;
            Difficulty = Properties.Settings.Default.Difficulty;
        }

        public void Save()
        {
            Properties.Settings.Default.PlaySounds = PlaySounds;
            Properties.Settings.Default.NumberOfChallenges = NumberOfChallenges;
            Properties.Settings.Default.Difficulty = Difficulty;
            Properties.Settings.Default.Save();
        }
    }
}
