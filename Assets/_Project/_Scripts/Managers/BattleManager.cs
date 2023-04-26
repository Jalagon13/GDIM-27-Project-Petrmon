using System.Collections;
using UnityEngine;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;

        private bool _battleActive;
        private PartyObject _opponentParty;

        public void StartBattle(PartyObject opponentParty)
        {
            _opponentParty = opponentParty;

            // Display Petrmon Roster count visually 
        }
    }
}
