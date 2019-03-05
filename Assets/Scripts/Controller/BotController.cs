using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class BotController : BaseController
	{
		public List<Bot> GetBotList { get; set; } = new List<Bot>();

		public void Init(int countBot)
		{
			var bot = Resources.Load<Bot>("Bot");
			for (var index = 0; index < countBot; index++)
			{
				var tempBot = Bot.Instantiate(bot,
					Patrol.GenericPoint(Main.Instance.Player),
					Quaternion.identity);

				tempBot.OnRemoveBotToListChange += RemoveBotToList;
				tempBot.Agent.avoidancePriority = index;
				tempBot.Target = Main.Instance.Player; // разных противников
				AddBotToList(tempBot);
			}
		}

		private void AddBotToList(Bot bot)
		{
			if (!GetBotList.Contains(bot))
			{
				GetBotList.Add(bot);
			}
		}
		private void RemoveBotToList(Bot bot)
		{
			if (GetBotList.Contains(bot))
			{
				GetBotList.Remove(bot);
			}
		}

		public override void OnUpdate()
		{
			if (!IsActive) return;
			foreach (var bot in GetBotList)
			{
				bot.Tick();
			}
		}
	}
}