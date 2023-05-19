namespace SuportesBalanceados;

public class Worker : BackgroundService
{
    //Comentários do dev:
    //Como não foi solicitado tratamento de exceção e/ou log detalhado do motivo da string ser inválida, eu não implementei.
    //Porém, caso fosse necessário, a função poderia logar informações como: qual caracter está inválido ou qual sua posição na string.

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string entrada = "([{()}])(){)}";
        bool isValid = IsStringValid(entrada);
        Console.WriteLine($"A sequência de colchetes '{entrada}' é {(isValid ? "válida" : "inválida")}.");
    }

    private static bool IsStringValid(string entrada)
    {
        //Definição dos caracteres válidos e seus respectivos pares.
        Dictionary<char, char> paresColchetes = new()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };

        Stack<char> pilhaColchetesEncontrados = new Stack<char>();

        foreach (char caracter in entrada)
        {
            if (paresColchetes.ContainsKey(caracter))
            {
                //Se o caracter for um colchete de abertura, apenas adiciona à pilha.
                pilhaColchetesEncontrados.Push(caracter);
            }
            else if (paresColchetes.ContainsValue(caracter))
            {
                //Se o caracter for um colchete de fechamento, verifica se corresponde ao colchete mais recente na pilha.
                if (pilhaColchetesEncontrados.Count == 0 || paresColchetes[pilhaColchetesEncontrados.Peek()] != caracter)
                {
                    return false; //Ordem de colchetes inválida.
                }

                pilhaColchetesEncontrados.Pop(); //Remove o colchete de abertura correspondente da pilha.
            }

            //Ignora outros caracteres que não sejam colchetes. Caso seja necessário retornar falso para este caso,
            //basta apenas descomentar o código abaixo:

            //else
            //{
            //    return false;
            //}
        }

        return pilhaColchetesEncontrados.Count == 0; //Retorna true se todos os colchetes tiverem pares correspondentes na pilha.
    }
}