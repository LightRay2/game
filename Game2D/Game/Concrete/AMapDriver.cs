using Game2D.Game.DataClasses;
using Game2D.Opengl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;
using AGeneration;

namespace Game2D.Game.Concrete
{
    class AMapDriver
    {
        AVertexMap v_map;
        ALightMap l_map;

        const int cell_size = 5;

        public AMapDriver()
        {
            v_map = new AVertexMap(65, 65, 1, 1, 30, 5);
            l_map = new ALightMap(v_map);
        }

        public void Process(List<Command> serverCommands, DStateMain sceneState, ref Frame resultFrame)
        {
            resultFrame.Add(render_map().ToArray());
        }

        List<Sprite> render_map()
        {
            List<Sprite> sprites = new List<Sprite>();
            
            for (int i = 0; i < l_map.width; i++)
                for (int j = 0; j < l_map.height; j++)
                {
                    sprites.Add(new Sprite(ESprite.grass,
                        new Vector2(new Point2(i * cell_size, j * cell_size)),
                        new Point2(cell_size, cell_size))); //adding ground

                    for (int direction = 0; direction < 4; direction++) //adding light
                    {
                        if (l_map.get(i, j, direction).light.ToString() == "1")
                            continue;

                        //finding texture by name
                        string sprite_name = ADirection.direction_to_char(direction) +  //direction
                            (l_map.get(i, j, direction).light * 10).ToString();    //light coefficient
                        
                        sprites.Add(new Sprite(sprite_by_name(sprite_name),
                            new Vector2(new Point2(i * cell_size, j * cell_size)),
                            new Point2(cell_size, cell_size)));
                    }
                }


            return sprites;
        }

        ESprite sprite_by_name(string s_name)
        {
            for (ESprite i = (ESprite)0; i != ESprite.end; i++)
            {
                if (i.ToString() == s_name)
                    return i;
            }
            return ESprite.end;
        }
    }
}
