import { Canvas } from "@react-three/fiber";
import { ContactShadows, Environment, OrbitControls } from "@react-three/drei";
import { Suspense, useEffect, useState } from "react";
import Wave from "../../public/Wave.jsx";

function AnimationWave() {
    const [autoRotateEnabled, setAutoRotateEnabled] = useState(true);

    useEffect(() => {
        const timer = setTimeout(() => {
            setAutoRotateEnabled(false);
        }, 7000);

        return () => clearTimeout(timer); // Cleanup
    }, []);

    return (
        <Canvas>
            <ambientLight />
            <OrbitControls
                enableZoom={false}
                autoRotate={autoRotateEnabled}
                autoRotateSpeed={9}
            />
            <Suspense fallback={null}>
                <Wave />
            </Suspense>
            <Environment preset="sunset" />
            <ContactShadows
                position={[0, -0.5, 0]}
                opacity={0.8}
                scale={80}
                blur={1}
                far={10}
                resolution={256}
                color="#000000"
            />
        </Canvas>
    );
}

export default AnimationWave;
