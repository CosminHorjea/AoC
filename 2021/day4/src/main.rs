use std::collections::hash_map;
use std::collections::hash_set;
use std::fs;
use std::io;

fn solve_board(board: &Vec<Vec<u32>>, numbers_called: &hash_set::HashSet<u32>, number: u32) {
    let mut result = 0;
    for i in 0..5 {
        for j in 0..5 {
            if !numbers_called.contains(&board[i][j]) {
                result += board[i][j];
            }
        }
    }
    result *= number;
    print!("We found it {}, with RustLang\n", result);
}

fn main() {
    let content = fs::read_to_string("day4.in").unwrap();
    // print!("{}", content);
    let mut lines: Vec<&str> = content.lines().collect::<Vec<&str>>();
    let numbers: Vec<u32> = lines[0]
        .split(',')
        .map(|x| x.parse::<u32>().unwrap())
        .collect::<Vec<u32>>();
    print!("Num:{}\n", numbers.len());
    lines.remove(0);
    //remove empty lines
    lines.retain(|&x| x != "");
    print!("Total Boards: {}\n", lines.len());
    let mut boards: Vec<&[&str]> = lines.chunks(5).collect::<Vec<&[&str]>>();
    let mut numbers_called: hash_set::HashSet<u32> = hash_set::HashSet::new();
    let mut found_sol = 0;
    for number in numbers {
        // if (found_sol == 1) {
        //     break;
        // }
        print!("Draw: {}\n", number);
        numbers_called.insert(number);
        if (numbers_called.len() < 5) {
            continue;
        }
        let mut boards_copy = boards.clone();
        for board in boards.iter() {
            let number_board: Vec<Vec<u32>> = board
                .clone()
                .iter()
                .map(|x| {
                    x.split_whitespace()
                        .map(|y| y.parse::<u32>().unwrap())
                        .collect::<Vec<u32>>()
                })
                .collect::<Vec<Vec<u32>>>();
            let mut flag = 1;
            for i in 0..5 {
                for j in 0..5 {
                    if !numbers_called.contains(&number_board[i][j]) {
                        flag = 0;
                        break;
                    }
                }
                if flag == 1 {
                    //remove borad from boards
                    if (boards.len() == 1) {
                        solve_board(&number_board, &numbers_called, number);
                        break;
                    }
                    boards_copy.retain(|&x| (x[0] != board[0]));
                    break;

                    let mut result = 0;
                    for i1 in 0..5 {
                        for j1 in 0..5 {
                            if !numbers_called.contains(&number_board[i1][j1]) {
                                result += number_board[i1][j1];
                            }
                        }
                    }
                    result *= number;
                    print!("Part1 {}, made possible with RustLang\n", result);
                    found_sol = 1;
                    break;
                }
                flag = 1;
            }
            for j in 0..5 {
                for i in 0..5 {
                    if !numbers_called.contains(&number_board[i][j]) {
                        flag = 0;
                        break;
                    }
                }
                if flag == 1 {
                    if (boards.len() == 1) {
                        solve_board(&number_board, &numbers_called, number);
                        break;
                    }

                    boards_copy.retain(|&x| (x[0] != board[0]));
                    break;
                    let mut result = 0;
                    for i1 in 0..5 {
                        for j1 in 0..5 {
                            if !numbers_called.contains(&number_board[i1][j1]) {
                                result += number_board[i1][j1];
                            }
                        }
                    }
                    result *= number;
                    print!("Part1 {}, made possible with RustLang\n", result);
                    found_sol = 1;
                    break;
                }
                flag = 1;
            }
        }
        boards = boards_copy.clone();
        boards_copy.clear();
        // if (boards.len() == 1) {
        //     let number_board: Vec<Vec<u32>> = boards[0]
        //         .clone()
        //         .iter()
        //         .map(|x| {
        //             x.split_whitespace()
        //                 .map(|y| y.parse::<u32>().unwrap())
        //                 .collect::<Vec<u32>>()
        //         })
        //         .collect::<Vec<Vec<u32>>>();
        //     let mut result = 0;
        //     for i in 0..5 {
        //         for j in 0..5 {
        //             if !numbers_called.contains(&number_board[i][j]) {
        //                 result += number_board[i][j];
        //             }
        //         }
        //     }
        //     result *= number;
        //     print!("We found it {}, with RustLang", result);
        //     break;
        // }
    }
}
// Part 1 35670
// Part 2 22704
