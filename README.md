# Hedy.Common.Library
Common library project Hedy




private static bool HasDuplicatedClauses(IEnumerable<Guid> clauses)
{
	 return !clauses.Select(it => it).All(new HashSet<Guid>().Add);
}