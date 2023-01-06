using UnityEngine;

namespace com.zibra.liquid.Samples
{
    internal class ZibraGravityManipulator : MonoBehaviour
    {
        private Solver.ZibraLiquid liquid;

        // Start is called before the first frame update
        private void Start()
        {
            liquid = GetComponent<Solver.ZibraLiquid>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                liquid.SolverParameters.Gravity.y = 9.81f;
                liquid.SolverParameters.Gravity.x = 0.0f;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                liquid.SolverParameters.Gravity.y = -9.81f;
                liquid.SolverParameters.Gravity.x = 0.0f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                liquid.SolverParameters.Gravity.y = 0.0f;
                liquid.SolverParameters.Gravity.x = 9.81f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                liquid.SolverParameters.Gravity.x = -9.81f;
                liquid.SolverParameters.Gravity.y = 0.0f;
            }

            if (Input.GetKey(KeyCode.O))
            {
                liquid.SolverParameters.Gravity.x = 0.0f;
                liquid.SolverParameters.Gravity.y = 0.0f;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                liquid.SolverParameters.Gravity *= 1.02f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                liquid.SolverParameters.Gravity *= 0.98f;
            }
        }
    }
}
