using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base()
        {

        }

        public Sequence(List<Node> children) : base(children)
        {

        }

        public override NodeState Evaluate()
        {
            bool childRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        childRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = childRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}


