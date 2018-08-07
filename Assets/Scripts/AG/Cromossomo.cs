﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cromossomo possui a codificação dos genes em binário.
 */
public class Cromossomo
{
	public BitArray[] genes;

	/**
	 * Quantidade de bits para cada gene.
	 */
	private const int qtdBits = 8;

	//
	// ─── CONSTRUTORES ───────────────────────────────────────────────────────────────
	//

	public Cromossomo(BitArray[] genesCodificados){
		this.genes = genesCodificados;
	}
	
	public Cromossomo(int qtdGenes, float[][] limitesInfSup)
	{
		genes = new BitArray[qtdGenes];

		codificar(limitesInfSup, qtdBits);
	}

	//
	// ─── CODIFICAÇÃO E DECODIFICAÇÃO ────────────────────────────────────────────────
	//

	/**
	 * Baseado no pseudocódigo fornecido
	 * na aula de codificação.
	 */
	private void codificar(float[][] limitesInfSup, int qtdBits)
	{
		for(int i = 0; i < genes.Length; i++)
		{
			float inf = limitesInfSup[i][0];
			float sup = limitesInfSup[i][1];

			float random = UnityEngine.Random.Range(inf, sup);

			float aux = ((random - inf) / (sup - inf)) * (Mathf.Pow(2, qtdBits) - 1);
			byte baux = Convert.ToByte(random);

			genes[i] = new BitArray(new byte[] { baux });
		}
	}

	/**
	 * TODO: Fazer a decodificação de acordo com o pseudocódigo.
	 * 
	 * TODO: Decodificar para int ao invés de byte.
	 */
	public byte[] decodificar()
	{
		byte[] bytes = new byte[genes.Length];

		for(int i = 0; i < genes.Length; i++)
			bytes[i] = ConverterBitArrayParaByte(genes[i]);

		return bytes;
	}

	//
	// ─── CONVERSORES ────────────────────────────────────────────────────────────────
	//

	/**
	 * https://stackoverflow.com/questions/45759398/byte-to-bitarray-and-back-to-byte
	 */
	public byte ConverterBitArrayParaByte(BitArray bits)
	{
		/**
		 * New byte[1] pq estamos usando apenas 8 bits
		 * para codificar nossos genes.
		 */
		var bytes = new byte[1];
    	bits.CopyTo(bytes, 0);
    	return bytes[0];
	}

	private String ConverterBitArrayParaString(BitArray bits)
	{
		String s = "";

		/**
		 * Tem que percorrer ao contrário porque ele
		 * armazena os bits menos significativos
		 * nos primeiros índices.
		 * 
		 * https://stackoverflow.com/questions/9066831/bitarray-returns-bits-the-wrong-way-around
		 */
		for(int i = bits.Length - 1; i >= 0; i--)
		{
			bool bit = bits[i];

			if(bit) s += "1";
			else    s += "0";
		}

		return s;
	}
	// ────────────────────────────────────────────────────────────────────────────────
}