namespace IxaCalc.Model
{
    using System.Collections.Generic;
    using System.Windows;
    using IxaCalc.Enums;

    /// <summary>
    /// 部隊
    /// </summary>
    public class Deck : ModelBase
    {
        #region フィールド
        /// <summary>
        /// デッキ上の武将リスト
        /// </summary>
        private List<DeckedBusho> _deckedBushos;

        /// <summary>
        /// 現在の兵種
        /// </summary>
        private SoldierTypes _currentSoldierType;

        /// <summary>
        /// 全攻撃力
        /// </summary>
        private double _totalAttack;

        /// <summary>
        /// 前防御力
        /// </summary>
        private double _totalDefence;

        /// <summary>
        /// 攻撃力バーの長さ
        /// </summary>
        private double _totalAttackBar;

        /// <summary>
        /// 防御力バーの長さ
        /// </summary>
        private double _totalDefenceBar;

        /// <summary>
        /// コスト1あたり攻撃力
        /// </summary>
        private double _totalAttackPerCost;

        /// <summary>
        /// コスト1あたり防御力
        /// </summary>
        private double _totalDefencePerCost;

        /// <summary>
        /// 全コスト
        /// </summary>
        private double _totalCost;

        /// <summary>
        /// 兵士数
        /// </summary>
        private int _totalSoldierNumber;

        /// <summary>
        /// コスト1あたりの兵士数
        /// </summary>
        private double _totalSoldierNumberPerCost;

        /// <summary>
        /// デッキのコントロールのVisible状態
        /// </summary>
        private Visibility[] _deckedVisibility = new Visibility[4];
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Deck()
        {
            _deckedBushos = new List<DeckedBusho>(4);
            UpdateDeckVisibility();
        }

        #region プロパティ
        /// <summary>
        /// デッキ内武将のリスト
        /// </summary>
        public List<DeckedBusho> DeckedBushos
        {
            get
            {
                return _deckedBushos;
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalAttack
        {
            get
            {
                return _totalAttack;
            }

            set
            {
                _totalAttack = value;
                OnPropertyChanged("TotalAttack");
            }
        }

        /// <summary>
        /// デッキの現在の兵種
        /// </summary>
        public SoldierTypes CurrentSoldierType
        {
            get
            {
                return _currentSoldierType;
            }

            set
            {
                if (_currentSoldierType != value)
                {
                    _currentSoldierType = value;
                    OnPropertyChanged("CurrentSoldierType");
                    OnPropertyChanged("CurrentSoldierTypeName");
                }
            }
        }

        /// <summary>
        /// デッキの現在の兵種名
        /// </summary>
        public string CurrentSoldierTypeName
        {
            get
            {
                return RankDictionary.Soldiers[_currentSoldierType].Name;
            }
        }

        /// <summary>
        /// 総防御
        /// </summary>
        public double TotalDefence
        {
            get
            {
                return _totalDefence;
            }

            set
            {
                _totalDefence = value;
                OnPropertyChanged("TotalDefence");
            }
        }

        /// <summary>
        /// 総攻撃力をゲージに見せる際の長さ (0~1)
        /// </summary>
        public double TotalAttackBar
        {
            get
            {
                return _totalAttackBar;
            }

            set
            {
                _totalAttackBar = value;
                OnPropertyChanged("TotalAttackBar");
            }
        }

        /// <summary>
        /// 総防御
        /// </summary>
        public double TotalDefenceBar
        {
            get
            {
                return _totalDefenceBar;
            }

            set
            {
                _totalDefenceBar = value;
                OnPropertyChanged("TotalDefenceBar");
            }
        }

        /// <summary>
        /// コスト1あたりの総攻撃
        /// </summary>
        public double TotalAttackPerCost
        {
            get
            {
                return _totalAttackPerCost;
            }

            set
            {
                _totalAttackPerCost = value;
                OnPropertyChanged("TotalAttackPerCost");
            }
        }

        /// <summary>
        /// デッキの総兵士数
        /// </summary>
        public int TotalSoldierNumber
        {
            get
            {
                return _totalSoldierNumber;
            }

            set
            {
                _totalSoldierNumber = value;
                OnPropertyChanged("TotalSoldierNumber");
            }
        }

        /// <summary>
        /// コスト1あたりの総兵士数
        /// </summary>
        public double TotalSoldierNumberPerCost
        {
            get
            {
                return _totalSoldierNumberPerCost;
            }

            set
            {
                _totalSoldierNumberPerCost = value;
                OnPropertyChanged("TotalSoldierNumberPerCost");
            }
        }

        /// <summary>
        /// コスト1あたりの総防御
        /// </summary>
        public double TotalDefencePerCost
        {
            get
            {
                return _totalDefencePerCost;
            }

            set
            {
                _totalDefencePerCost = value;
                OnPropertyChanged("TotalDefencePerCost");
            }
        }

        /// <summary>
        /// デッキの全コスト
        /// </summary>
        public double TotalCost
        {
            get
            {
                return _totalCost;
            }

            set
            {
                _totalCost = value;
                OnPropertyChanged("TotalCost");
            }
        }

        /// <summary>
        /// デッキ内コントロールを見せるかどうか
        /// </summary>
        public Visibility[] DeckedVisibility
        {
            get
            {
                return _deckedVisibility;
            }
        }
        #endregion

        #region メソッド
        #region public
        /// <summary>
        /// デッキに武将を入れる
        /// </summary>
        /// <param name="busho">追加する武将</param>
        public void Add(Busho busho)
        {
            if (DeckedBushos.Count < 4 && !this.IsContain(busho))
            {
                var decked = new DeckedBusho(busho, _currentSoldierType);
                DeckedBushos.Add(decked);
                this.CalculateTotalStatus();
                UpdateDeckVisibility();
                OnPropertyChanged("DeckedBushos");
            }
        }

        /// <summary>
        /// デッキから武将を削除
        /// </summary>
        /// <param name="index">削除する武将の場所</param>
        public void Remove(int index)
        {
            if (index < DeckedBushos.Count)
            {
                DeckedBushos.RemoveAt(index);
                this.CalculateTotalStatus();
                UpdateDeckVisibility();
                OnPropertyChanged("DeckedBushos");
            }
        }

        /// <summary>
        /// デッキの最後の武将を削除
        /// </summary>
        public void RemoveLast()
        {
            if (DeckedBushos.Count > 0)
            {
                Remove(DeckedBushos.Count - 1);
            }
        }

        /// <summary>
        /// ランクアップ
        /// </summary>
        /// <param name="index">武将のデッキ上の位置</param>
        public void RankUp(int index)
        {
            DeckedBushos[index].RankUp();
            this.CalculateTotalStatus();
        }

        /// <summary>
        /// ランクダウン
        /// </summary>
        /// <param name="index">武将のデッキ上の位置</param>
        public void RankDown(int index)
        {
            DeckedBushos[index].RankDown();
            this.CalculateTotalStatus();
        }

        /// <summary>
        /// 現在の兵種を切り替える
        /// </summary>
        /// <param name="type">切り替え後の兵種</param>
        public void SwitchSoldierType(SoldierTypes type)
        {
            CurrentSoldierType = type;
            foreach (var decked in DeckedBushos)
            {
                decked.CurrentSoldierType = _currentSoldierType;
            }

            this.CalculateTotalStatus();
        }
        #endregion

        #region private

        /// <summary>
        /// デッキ内コントロールのVisivilityを更新する
        /// </summary>
        private void UpdateDeckVisibility()
        {
            var bushonum = DeckedBushos.Count;
            for (int i = 0; i < 4; i++)
            {
                _deckedVisibility[i] = i < bushonum ? Visibility.Visible : Visibility.Collapsed;
            }

            OnPropertyChanged("DeckedVisibility");
        }

        /// <summary>
        /// 全ステータス値を更新する
        /// </summary>
        private void CalculateTotalStatus()
        {
            double totalAtk = 0;
            double totalDef = 0;
            double totalCost = 0;
            int soldier = 0;
            var type = CurrentSoldierType;
            foreach (var decked in DeckedBushos)
            {
                soldier += decked.OriginBusho.SoldierNumber;
                totalAtk += decked.ActualAttack;
                totalDef += decked.ActualDefence;
                totalCost += decked.OriginBusho.Cost;
            }

            TotalAttack = totalAtk;
            TotalDefence = totalDef;
            TotalAttackBar = TotalAttack > 400000 ? 1.0 : TotalAttack / 400000;
            TotalDefenceBar = TotalDefence > 400000 ? 1.0 : TotalDefence / 400000;
            TotalCost = totalCost;
            TotalSoldierNumber = soldier;
            TotalSoldierNumberPerCost = TotalSoldierNumber / TotalCost;
            TotalAttackPerCost = TotalAttack / TotalCost;
            TotalDefencePerCost = TotalDefence / TotalCost;
        }

        /// <summary>
        /// すでにデッキに武将が入っているかを調べる
        /// </summary>
        /// <param name="cmpBusho">調査対象武将</param>
        /// <returns>入っていればtrue</returns>
        private bool IsContain(Busho cmpBusho)
        {
            bool cond = false;
            foreach (var decked in DeckedBushos)
            {
                if (decked.OriginBusho.Id == cmpBusho.Id)
                {
                    cond = true;
                    break;
                }
            }

            return cond;
        }
        #endregion
        #endregion
    }
}
