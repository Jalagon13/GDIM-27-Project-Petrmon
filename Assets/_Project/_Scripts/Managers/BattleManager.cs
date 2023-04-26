using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private GameObject _playerAssets;
        [SerializeField] private GameObject _opponentAssets;

        private Canvas _battleCanvas;
        private GridLayoutGroup _fightButtonLayout;

        protected override void Awake()
        {
            base.Awake();

            _battleCanvas = transform.GetChild(0).GetComponent<Canvas>();
            _fightButtonLayout = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            _battleCanvas.gameObject.SetActive(false);
            _playerAssets.SetActive(false);
            _opponentAssets.SetActive(false);
        }

        public void StartBattle(PartyObject opponentParty)
        {
            InitializeMoves();
            ShowAssets();
        }

        private void ShowAssets()
        {
            _battleCanvas.gameObject.SetActive(true);
            _playerAssets.SetActive(true);
            _opponentAssets.SetActive(true);
        }

        private void InitializeMoves()
        {
            var petrmonIndex = 0;
            var index = 0;

            foreach(Transform child in _fightButtonLayout.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    fightButton.UpdateFightButton(_playerParty.Party[petrmonIndex].MoveSet.Set[index]);
                    index++;
                }
            }
        }
    }
}
