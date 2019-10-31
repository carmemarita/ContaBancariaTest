
### EXTRATO
ENDPOINT: /api/Conta
METHOD: GET

RETURN: {
    "listaOperacoes": [
        {
            "valor": 300.2,
            "tipoEvento": 0,
            "tipo": "Deposito",
            "dateEvento": "31/10/2019 10:05",
            "cpfDetinatario": "999.999.999-99",
            "custoTaxaDeMovimentacao": 3.0
        }
    ],
    "saldo": 2886.25
}

### DEPÓSITO
ENDPOINT: /api/Conta/AddDeposito/{value}
METHOD: Post
NO RETURN 

## SAQUE 
ENDPOINT: /api/Conta/AddSaque/{value}
METHOD: Post
NO RETURN 

### TRANSFERÊNCIA
ENDPOINT: /api/Conta/AddTransferencia
METHOD: Post
NO RETURN 
