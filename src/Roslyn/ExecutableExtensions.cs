namespace CER.Roslyn
{
    using System;
    using System.Text;

    public static class ExceptionExtensions
    {
        public static string DetailedMessage(this Exception e)
        {
            return e.detailedMessage(0, new StringBuilder());
        }
        private static string detailedMessage(this Exception e, int depth, StringBuilder sb)
        {
            while (e.InnerException != null)
            {
                sb.Append(e.InnerException.detailedMessage(depth + 1, sb));
            }
            for (int i = 0; i < depth; i++)
            {
                sb.Append("Inner-");
            }
            sb.AppendLine(e.GetType().FullName);
            sb.AppendLine(e.Message);
            sb.AppendLine(e.StackTrace);
            return sb.ToString();
        }
    }
}

