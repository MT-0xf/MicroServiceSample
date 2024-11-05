package service;

import config.Config;
import interfaces.IUploadService;
import mainBusiness.MainBusinessGrpc;
import mainBusiness.MainBusinessOuterClass;
import org.springframework.stereotype.Service;

/**
 * アップロードサービス
 */
@Service
public class UploadService implements IUploadService {
    /**
     * が増アップロード
     * @param title　画像タイトル
     * @param imageFile 画像ファイル　
     * @return アップロード可否
     */
    @Override
    public MainBusinessOuterClass.PostImageReply postImage(String title, String imageFile) {
        MainBusinessGrpc.MainBusinessBlockingStub stub = MainBusinessGrpc.newBlockingStub(Config.channel);

        MainBusinessOuterClass.PostImageRequest request = MainBusinessOuterClass.PostImageRequest.newBuilder()
                .setTitle(title)
                .setImageFile(imageFile)
                .build();

        MainBusinessOuterClass.PostImageReply reply = stub.postImage(request);
        return reply;
    }
}
