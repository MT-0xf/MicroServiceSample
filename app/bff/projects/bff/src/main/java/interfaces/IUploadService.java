package interfaces;

import mainBusiness.MainBusinessOuterClass;
import org.springframework.graphql.data.method.annotation.Argument;

/**
 * アップロードサービスインターフェース
 */
public interface IUploadService {
    /**
     * 画像アップロード
     * @param title　画像タイトル
     * @param imageFile 画像ファイル　
     * @return アップロード可否
     */
    public MainBusinessOuterClass.PostImageReply postImage(@Argument final String title, @Argument final String imageFile);
}
