using GroupCloning.Database;
using GroupCloning.Database.Models;
using GroupCloning.Database.Repositories.GroupRepo;

namespace GroupCloning;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Introdu numarul primei grupe:");
        var sourceGroupNumber = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()); 
        Console.WriteLine("Introdu numarul celei de a doua grupe:");
        var destinationGroupNumber = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        await Clone(sourceGroupNumber, destinationGroupNumber);
        await ShowResult(destinationGroupNumber);
    }

    private static async Task Clone(int sourceGroupNumber, int destinationGroupNumber)
    {
        using var context = new GroupCloningDbContext();
        var groupRepo = new GroupRepo(context);
        
        // Luam listele cu grupele de care avem nevoie si ne asiguram ca exista elemente care au groupNumber-ul respectiv
        var sourceGroupList = await groupRepo.GetByGroupNumberAsync(sourceGroupNumber);
        if (sourceGroupList.Count == 0)
        {
            throw new Exception($"There are no groups with the number {sourceGroupNumber} found");
        }

        var destinationGroupList = await groupRepo.GetByGroupNumberAsync(destinationGroupNumber);
        if (destinationGroupList.Count == 0)
        {
            throw new Exception($"There are no groups with the number {destinationGroupNumber} found");
        }

        // Aici am indexat dupa identifierInGroup deoarece este unic grupei in care se afla
        var destinationGroupDict = destinationGroupList.ToDictionary(group => group.IdentifierInGroup);

        //Cu ajutorul acestei liste o sa ma asigur ca nu updatez un obiect de mai multe ori si voi memora doar identifierInGroup fiind unic
        var updatedGroups = new List<int>();
        
        // Aici am ales sa foloesc dictionare pentru a eficientiza accesarea elementelor deoarece doresc sa aflu toate elementele care au proprietatile la fel
        // Iar daca nu sunt la fel atunci le adaugam in lista
        foreach (var sourceGroup in sourceGroupList)
        {
            // Daca vreun element din destinationGroup are acelasi identifierInGroup ca sourceGroup atunci se verifica si restul proprietatilor mai putin groupNumber
            // Daca nu sunt la fel atunci dam update proprietatilor obiectului db
            // In final adaugam valoarea identifierInGroup in lista updatedGroups
            if (destinationGroupDict.TryGetValue(sourceGroup.IdentifierInGroup, out var destinationGroup))
            {
                if (!sourceGroup.Equals(destinationGroup))
                {
                    destinationGroup.Prop1 = sourceGroup.Prop1;
                    destinationGroup.Prop2 = sourceGroup.Prop2;
                    destinationGroup.Prop3 = sourceGroup.Prop3;
                    groupRepo.Update(destinationGroup);
                }
                updatedGroups.Add(sourceGroup.IdentifierInGroup);
            }
        }

        for (int i = 0; i < sourceGroupList.Count; i++)
        {
            var sourceGroup = sourceGroupList[i];
            // Verificam daca grupul a fost updatat deja
            if(updatedGroups.Contains(sourceGroup.IdentifierInGroup))
                continue;
            
            // Aici luam un grup din cele destinate la care sa le dam update
            for (int j = 0; j < destinationGroupList.Count; j++)
            {
                var destinationGroup = destinationGroupList[j];
                // Facem aceeasi verificare sa nu fi fost updated
                // Daca a fost, continuam
                if(updatedGroups.Contains(destinationGroup.IdentifierInGroup))
                    continue;
                
                // Actualizam valorile grupului
                destinationGroup.IdentifierInGroup = sourceGroup.IdentifierInGroup;
                destinationGroup.Prop1 = sourceGroup.Prop1;
                destinationGroup.Prop2 = sourceGroup.Prop2;
                destinationGroup.Prop3 = sourceGroup.Prop3;
                updatedGroups.Add(sourceGroup.IdentifierInGroup);
                groupRepo.Update(destinationGroup);
                // Iesim afara din loop pentru a nu face unnecessary updates
                break;
            }

            if (!updatedGroups.Contains(sourceGroup.IdentifierInGroup))
            {
                var tempDestinationGroup = new Group()
                {
                    GroupNumber = destinationGroupNumber,
                    IdentifierInGroup = sourceGroup.IdentifierInGroup,
                    Prop1 = sourceGroup.Prop1,
                    Prop2 = sourceGroup.Prop2,
                    Prop3 = sourceGroup.Prop3,  
                };
                updatedGroups.Add(sourceGroup.IdentifierInGroup);
                await groupRepo.AddAsync(tempDestinationGroup);
            }
        }

        // Stergem restul grupurilor ramase care nu au fost updated
        for (int i = sourceGroupList.Count; i < destinationGroupList.Count; i++)
        {
            var destinationGroup = destinationGroupList[i];
            groupRepo.Remove(destinationGroup);
        }
        
        await groupRepo.SaveChangesAsync();
    }


    private static async Task ShowResult(int destinationGroupNumber)
    {
        using var context = new GroupCloningDbContext();
        var groupRepo = new GroupRepo(context);
        var destinationGroupList = await groupRepo.GetByGroupNumberAsync(destinationGroupNumber);
        foreach (var group in destinationGroupList)
        {
            Console.WriteLine(group.ToString());
        }
    }
    
}