enum Op<T>{
    Lis,Id(T)
}

type ChildNode<T> = Option<Box<BTNode<T>>>;

struct BTNode<T>{
    parent:Option<Box<BTNode<T>>>,
    left:ChildNode<T>,
    right: ChildNode<T>,
    op: Op<T>
}
struct BinaryTree<T>{
    head: Option<BTNode<T>>
}

impl BTNode<i32>{
    pub fn new(parent:Option<Box<BTNode<i32>>>,left:ChildNode<i32>,right:ChildNode<i32>,op:Op<i32>)->BTNode<i32>{
        BTNode{
            parent,
            left,
            right,
            op
        }
    }
}

fn LisNode(parent:Option<Box<BTNode<i32>>>,left:ChildNode<i32>,right:ChildNode<i32>,op:Op<i32>)->BTNode<i32>{
    BTNode{
        parent,
        left,
        right,
        op
    }
}
fn IdNode(parent:Option<Box<BTNode<i32>>>,op:Op<i32>)->BTNode<i32>{
    BTNode{
        parent,
        left:None,
        right:None,
        op
    }
}

impl BinaryTree<i32> {
    pub fn new(head: BTNode<i32>) -> Self {
        BinaryTree{
            head: Some(head)
        }
    }

    pub fn collapse(node: &Box<BTNode<i32>>)-> i32 {
        42
    }
}

fn main() {
    let bt = BinaryTree::new(
       LisNode(None,None,None,Op::Id(42))
    );
    println!("{}", BinaryTree::collapse(&Box::new(bt.head.unwrap())));
}
