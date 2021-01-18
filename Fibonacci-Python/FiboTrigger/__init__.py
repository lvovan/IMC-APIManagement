import logging
import json
import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    n = req.params.get('n')

    if n:
        return json.dumps({ "result": str(fibo((int(n)))) })
    else:
        return func.HttpResponse(
             "Missing parameter n",
             status_code=400
        )

def fibo(n):
    if n == 0:
        return 0
    if n == 1:
        return 1
    return fibo(n - 1) + fibo(n-2)
