﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Models;

public class Transaction
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Date { get; set; }
    public decimal value { get; set; }

}

public enum TransactionType
{
    Income,
    Expenses

}