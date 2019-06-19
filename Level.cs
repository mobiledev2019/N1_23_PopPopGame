using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

	private int number;
	private int star;

	public Level(int number, int star){
		this.number = number;
		this.star = star;
	}

	public int getNumber(){
		return this.number;
	}

	public int getStar(){
		return this.star;
	}
    public void setStar(int star)
    {
        this.star = star;
    }
}
