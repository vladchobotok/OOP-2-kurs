using System;

namespace Task_4
{
    class Program
    {

        static void Main(string[] args)
        {
            Tree tree = new NewTree();
            tree = new TreeWithStars(tree);
            tree = new TreeWithSpheres(tree);
            tree = new TreeUsingGarland(tree);
            Console.WriteLine("Is our tree decorated with stars? {0}", tree.stars);
            Console.WriteLine("Is our tree decorated with bells? {0}", tree.bells);
            Console.WriteLine("Is our tree decorated with spheres? {0}", tree.spheres);
            Console.WriteLine("Is our tree using garland? {0}", tree.useGarland());

            Console.ReadLine();
        }
        abstract class Tree
        {
            public Tree(bool stars, bool bells, bool spheres)
            {
                this.stars = stars;
                this.bells = bells;
                this.spheres = spheres;
            }
            public bool stars { get; protected set; }
            public bool bells { get; protected set; }
            public bool spheres { get; protected set; }
            public abstract bool useGarland();
        }

        class NewTree : Tree
        {
            public NewTree() : base(false, false, false)
            { }
            public override bool useGarland()
            {
                return false;
            }
        }

        abstract class TreeDecorator : Tree
        {
            protected Tree tree;
            public TreeDecorator(bool stars, bool bells, bool spheres, Tree tree) : base(stars, bells, spheres)
            {
                this.tree = tree;
            }
        }

        class TreeUsingGarland : TreeDecorator
        {
            public TreeUsingGarland(Tree tree)
                : base(tree.stars, tree.bells, tree.spheres, tree)
            { }

            public override bool useGarland()
            {
                return true;
            }
        }

        class TreeWithStars : TreeDecorator
        {
            public TreeWithStars(Tree tree)
                : base(true, tree.bells, tree.spheres, tree)
            { }

            public override bool useGarland()
            {
                return tree.useGarland();
            }
        }
        class TreeWithBells : TreeDecorator
        {
            public TreeWithBells(Tree tree)
                : base(tree.stars, true, tree.spheres, tree)
            { }

            public override bool useGarland()
            {
                return tree.useGarland();
            }
        }
        class TreeWithSpheres : TreeDecorator
        {
            public TreeWithSpheres(Tree tree)
                : base(tree.stars, tree.bells, true, tree)
            { }

            public override bool useGarland()
            {
                return tree.useGarland();
            }
        }
    }
}
