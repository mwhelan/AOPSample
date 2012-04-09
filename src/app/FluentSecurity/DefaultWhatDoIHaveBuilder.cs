using System.Linq;
using System.Text;

namespace FluentSecurity
{
	public class DefaultWhatDoIHaveBuilder : IWhatDoIHaveBuilder
	{
		public string WhatDoIHave(ISecurityConfiguration configuration)
		{
			var builder = new StringBuilder();

			builder.AppendFormat("Ignore missing configuration: {0}", configuration.IgnoreMissingConfiguration);

			builder.AppendLine().AppendLine().AppendLine("------------------------------------------------------------------------------------").AppendLine();

			foreach (var policyContainer in configuration.PolicyContainers.OrderBy(x => x.ActionName).OrderBy(x => x.ControllerName))
			{
				builder.AppendFormat(
					"{0} > {1}{2}",
					policyContainer.ControllerName,
					policyContainer.ActionName,
					policyContainer.GetPolicies().ToText()
					);
				builder.AppendLine().AppendLine();
			}

			builder.Append("------------------------------------------------------------------------------------");

			return builder.ToString();
		}
	}
}