using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class GomokuRuleOnCommand : CommandR
    {
        private GomokuRule rule;

        public GomokuRuleOnCommand(GomokuRule rule)
        {
            this.rule = rule;
        }

        public bool finish()
        {
           return rule.defindFinishRule(GameData.Instance.x,GameData.Instance.y);
        }

        public int[] moveCom()
        {
            return rule.defineMovementRuleforCom(PlayerData.Instance.difficulty);
        }
      
    }
}
