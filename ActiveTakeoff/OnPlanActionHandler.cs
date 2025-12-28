using System;

namespace QuoterPlan
{
	public delegate void OnPlanActionHandler(PlansNavigator.PlansActionEnum action, Plan plan, int index, int count);
}
