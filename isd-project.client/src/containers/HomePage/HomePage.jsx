import Header from "./Header";
import Footer from "./Footer"
import bhsk from "../../assets/bhsk.jpg"
import bhsksp from "../../assets/bhsksp.jpeg"

const HomePage = () => {
    return (
        <div className="bg-slate-100">
            <Header />
            <section className="font-[Inter]">

                <div className=" w-full h-650 mb-10">
                    <img src={bhsk} className="w-full h-650 blur z-0 absolute"/>
                    <h1 className="flex items-center justify-center w-full h-full z-20 relative text-5xl">
                        Bao hiem suc khoe
                    </h1>
                </div>

                <h1 className="flex items-center justify-center w-full h-full text-5xl py-10">
                    Cac goi bao hiem
                </h1>
                <div class="w-3/4 grid grid-cols-3 gap-20 mx-auto ">
                    <div className="bg-white h-510 rounded p-5 shadow-xl shadow-slate-300 hover:shadow-slate-400 duration-300 cursor-pointer ">
                        <img src={bhsksp} className="w-full h-2/4 rounded"/>
                        <p className="pt-3">
                            11/12/2023
                        </p>
                        <h2 className="flex item-center justify-center w-full text-4xl">
                            Goi bao hiem gi do
                        </h2>
                        <p className="">
                            Lorem, ipsum dolor sit amet consectetur adipisicing elit. Odio aliquam eveniet laborum aut fugiat repellendus ad ducimus. Id odio voluptate, impedit iusto eius facere doloremque, reprehenderit tempora doloribus aspernatur iure.
                        </p>
                    </div>

                    <div className="bg-white h-510 rounded p-5 shadow-xl shadow-slate-300 hover:shadow-slate-400 duration-300 cursor-pointer">
                        <img src={bhsksp} className="w-full h-2/4 rounded"/>
                        <p className="pt-3">
                            11/12/2023
                        </p>
                        <h2 className="flex item-center justify-center w-full text-4xl">
                            Goi bao hiem gi do
                        </h2>
                        <p>
                            Lorem, ipsum dolor sit amet consectetur adipisicing elit. Odio aliquam eveniet laborum aut fugiat repellendus ad ducimus. Id odio voluptate, impedit iusto eius facere doloremque, reprehenderit tempora doloribus aspernatur iure.
                        </p>
                    </div>

                    <div className="bg-white h-510 rounded p-5 shadow-xl shadow-slate-300 hover:shadow-slate-400 duration-300 cursor-pointer">
                        <img src={bhsksp} className="w-full h-2/4 rounded"/>
                        <p className="pt-3">
                            11/12/2023
                        </p>
                        <h2 className="flex item-center justify-center w-full text-4xl">
                            Goi bao hiem gi do
                        </h2>
                        <p>
                            Lorem, ipsum dolor sit amet consectetur adipisicing elit. Odio aliquam eveniet laborum aut fugiat repellendus ad ducimus. Id odio voluptate, impedit iusto eius facere doloremque, reprehenderit tempora doloribus aspernatur iure.
                        </p>
                    </div>

                </div>
                <button className="bg-red-500 mx-auto flex item-center justify-center mt-20 px-5 py-3 text-2xl rounded-3xl text-white shadow-lg shadow-slate-400 
                                    hover:bg-red-700 active:bg-red-700 active:shadow-lg duration-300 mb-10">
                        Xem them
                </button>
            </section>
            <Footer />
        </div>
    )
}

export default HomePage