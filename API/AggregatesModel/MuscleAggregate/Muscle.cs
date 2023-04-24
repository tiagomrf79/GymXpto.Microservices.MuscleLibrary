using API.SeedWork;

namespace API.AggregatesModel.MuscleAggregate;

public class Muscle : Entity, IAggregateRoot
{
    private readonly List<string> _synonyms = new List<string>();

    public Guid MuscleId { get; private set; }
    public string MuscleName { get; private set; } = default!;
    public IReadOnlyCollection<string> Synonyms => _synonyms.AsReadOnly();
    public byte[]? Image { get; private set; }

    public void AddSynonym(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        
        var existingSynonym = _synonyms.Where(s => s.Equals(name));
        if (existingSynonym.Any())
            throw new InvalidOperationException($"It already exists a synonym with the name '{name}' for this muscle.");

        _synonyms.Add(name);
    }

    public void RemoveSynonym(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        var synonymToRemove = _synonyms.FirstOrDefault(s => s.Equals(name));
        if (synonymToRemove == null)
            throw new ArgumentException($"No synonym was found with the name '{name}'.");

        _synonyms.Remove(synonymToRemove);
    }
}
