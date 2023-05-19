namespace SuportesBalanceados;

public class Worker : BackgroundService
{
    //Coment�rios do dev:
    //Como n�o foi solicitado tratamento de exce��o e/ou log detalhado do motivo da string ser inv�lida, eu n�o implementei.
    //Por�m, caso fosse necess�rio, a fun��o poderia logar informa��es como: qual caracter est� inv�lido ou qual sua posi��o na string.

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string entrada = "([{()}])(){)}";
        bool isValid = IsStringValid(entrada);
        Console.WriteLine($"A sequ�ncia de colchetes '{entrada}' � {(isValid ? "v�lida" : "inv�lida")}.");
    }

    private static bool IsStringValid(string entrada)
    {
        //Defini��o dos caracteres v�lidos e seus respectivos pares.
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
                //Se o caracter for um colchete de abertura, apenas adiciona � pilha.
                pilhaColchetesEncontrados.Push(caracter);
            }
            else if (paresColchetes.ContainsValue(caracter))
            {
                //Se o caracter for um colchete de fechamento, verifica se corresponde ao colchete mais recente na pilha.
                if (pilhaColchetesEncontrados.Count == 0 || paresColchetes[pilhaColchetesEncontrados.Peek()] != caracter)
                {
                    return false; //Ordem de colchetes inv�lida.
                }

                pilhaColchetesEncontrados.Pop(); //Remove o colchete de abertura correspondente da pilha.
            }

            //Ignora outros caracteres que n�o sejam colchetes. Caso seja necess�rio retornar falso para este caso,
            //basta apenas descomentar o c�digo abaixo:

            //else
            //{
            //    return false;
            //}
        }

        return pilhaColchetesEncontrados.Count == 0; //Retorna true se todos os colchetes tiverem pares correspondentes na pilha.
    }
}