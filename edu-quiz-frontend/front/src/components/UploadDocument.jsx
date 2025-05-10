import React, {useRef, useState} from 'react';
import CustomCircleButton from "@/components/CustomCircleButton.jsx";

function UploadDocument({btnText, apiEndpoint}) {
    const fileInputRef = useRef(null);
    const [isUploading, setIsUploading] = useState(false);

    const handleButtonClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = async (event) => {
        const file = event.target.files[0];
        if (file) {
            console.log('Selected file:', file);

            const formData = new FormData();
            formData.append('file', file);

            try {
                setIsUploading(true);

                const response = await fetch(apiEndpoint, {
                    method: 'POST',
                    body: formData,
                    headers: {
                        // 'Accept': 'application/json',
                    },
                });

                if (!response.ok) {
                    throw new Error('File upload failed');
                }

                const result = await response.json();
                console.log('Upload Done:', result);
            } catch (error) {
                console.error('Error uploading file:', error);
            } finally {
                setIsUploading(false);
            }
        }
    };

    return (
        <div className="form-group mb-3">
            <CustomCircleButton
                btnText={isUploading ? 'Uploading...' : btnText}
                onClick={handleButtonClick}
                disabled={isUploading}
            />
            <input
                type="file"
                ref={fileInputRef}
                style={{ display: 'none' }}
                onChange={handleFileChange}
            />
        </div>
    );
}

export default UploadDocument;