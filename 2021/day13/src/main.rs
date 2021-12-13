use std::{collections::HashSet, io::BufRead};

fn main() {
    let content = include_str!("../day13.in");
    let points = content.lines().take_while(|x|x.len() > 1).collect::<Vec<&str>>();
    let mut points_set: HashSet<(u32,u32)> = HashSet::new();
    for (x,y) in points.iter().map(|pair|{
        (pair.split_once(",").unwrap().0.parse::<u32>().unwrap(),
        pair.split_once(",").unwrap().1.parse::<u32>().unwrap())
    }) {
       points_set.insert((x,y));
    }
    let folds:Vec<&str> = content.lines().skip_while(|x| x.len() > 1).skip(1).map(
        |line| line.split_whitespace().nth(2).unwrap()
    ).collect();
    let mut overlaps =0;
    for f in folds{
        let (axis,pos) = (f.split_once("=").unwrap().0,f.split_once("=").unwrap().1.parse::<u32>().unwrap());
        let mut points_to_add :HashSet<(u32,u32)> = HashSet::new();
        match axis {
            "x"=>{
                for (x,y) in points_set.iter().filter(|(x,y)| x > &pos) {
                    if points_set.contains(&(pos - (*x - pos),*y)){
                        overlaps+=1;
                    }else{
                        points_to_add.insert((pos - (*x - pos),*y));
                    }
                }
                points_set.retain(|(x,_)| x < &pos);
                println!("Matched x");
            },
            "y" => {
                for (x,y) in points_set.iter().filter(|(x,y)| y > &pos) {
                    if points_set.contains(&(*x,pos - (*y - pos))){
                        overlaps+=1;
                    }else{
                        points_to_add.insert((*x,pos - (*y - pos)));
                    }
                }
                points_set.retain(|(_,y)| y < &pos);
                println!("Matched y");
            },

            _ => panic!("Mom come pick me up")
        }
        for (x,y) in points_to_add{
            points_set.insert((x,y));
        }
        // println!("Size: {}",points_set.len())
        
    }
    for p in points_set.iter() {
        println!("{:?}",p);
    }
    for j in 0..10 {
        for i in 0..40 {
            if(points_set.contains(&(i,j))){
                print!("#");
            }else{
                print!(".");
            }
        }
        println!("");
    }
}
//FJAHJGAI not good\
// it was a H at the end