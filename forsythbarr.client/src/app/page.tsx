// pages/gallery.js
'use client'
import Image from 'next/image';
import Link from 'next/link';
import styled from 'styled-components'

const images =
    [
        '029e095be0c8cb2c74e124c5b843c6f8.JPG',
        '13bb59a04fb9052b27a021970929b809.jpg',
        '31fe8d84af421146ae4e25ecb0e21c49.jpg',
        '35e79ae981730e9313926f63738124cc.JPG',
        '38d73865ecf7cce9f3d310dfd1c84eb3.JPG',
        'e48bcdc3af4d30c638e2f158e1107d52.JPG',
        'IMG_4177.PNG',
        'IMG_4178.PNG',
        'IMG_4179.PNG',
        'IMG_4215.PNG',
        'IMG_4222.JPG',
        'IMG_4441.PNG',
        'IMG_4442.PNG',
        'IMG_4443.PNG',
        'IMG_4444.PNG',
        'IMG_4445.PNG',
        'IMG_4446.PNG',
        'IMG_4447.PNG',
        'IMG_4448.JPG',
        'IMG_4449.PNG',
        'IMG_4453.PNG',
        'IMG_4454.PNG',
        'IMG_4462.PNG',
        'IMG_4463.PNG',
        'IMG_4464.PNG',
        'IMG_4465.PNG',
        'IMG_4466.PNG'
    ]

const messages = [
    "吃饭的小宝好美！！！",
    "这件蓝色的衣服我最喜欢你穿",
    "小宝封神可爱的一张不用多说了",
    "可惜圣诞节没有跟你一起过",
    "小鹿角还会发光呢哈哈哈",
    "赶紧多吃点回到这张的状态",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "哈哈哈哈哈哈哈哈哈哈哈哈",
    "********private*********",
    "********private*********",
    "********private*********",
    "********private*********",
    "********private*********",
]

const Title = styled.h1`
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
    text-align: center;
    margin-bottom: 1rem;
`
const Home = () => {
    return (
        <div className={"flex flex-col"}>
            <Title>
                情人节快乐小宝！！&#x2764;&#xFE0F;我永远爱你！！！
            </Title>
            <div className="grid gap-4 grid-cols-2 md:grid-cols-3 lg:grid-cols-4">

                {images.map((image, index) => (
                        <div className="relative h-48 md:h-64 overflow-hidden rounded-lg">
                            <Image
                                src={`/bao/${image}`}
                                alt={`Image ${index + 1}`}
                                layout="fill"
                                objectFit="cover"
                                onClick={()=>{alert(messages[index])}}
                            />
                        </div>
                ))}
            </div>
        </div>

    );
};

export default Home;
